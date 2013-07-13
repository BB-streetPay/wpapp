using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using StreetPayWP.ViewModels;
using Microsoft.Phone.Tasks;
using System.ComponentModel;
using ZXing;
using Microsoft.Devices;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace StreetPayWP.Views
{
    public class PhotoCameraLuminanceSource : BaseLuminanceSource
    {
        public byte[] PreviewBufferY
        {
            get
            {
                return luminances;
            }
        }

        public PhotoCameraLuminanceSource(int width, int height)
            : base(width, height)
        {
            luminances = new byte[width * height];
        }

        internal PhotoCameraLuminanceSource(int width, int height, byte[] newLuminances)
            : base(width, height)
        {
            luminances = newLuminances;
        }

        protected override LuminanceSource CreateLuminanceSource(byte[] newLuminances, int width, int height)
        {
            return new PhotoCameraLuminanceSource(width, height, newLuminances);
        }
    }


    public partial class Scan : PhoneApplicationPage
    {
        private IBarcodeReader reader;
        private PhotoCamera photoCamera;
        private readonly WriteableBitmap dummyBitmap = new WriteableBitmap(1, 1);
        private PhotoCameraLuminanceSource luminance;

        ScanVM viewModel = new ScanVM();
        private DispatcherTimer timer;
        public Scan()
        {
            InitializeComponent();
            DataContext = viewModel;
            Loaded += Scan_Loaded;
        }

        void Scan_Loaded(object sender, RoutedEventArgs e)
        {
            if (photoCamera == null)
            {
                photoCamera = new PhotoCamera();
                photoCamera.Initialized += OnPhotoCameraInitialized;
                previewVideo.SetSource(photoCamera);

                CameraButtons.ShutterKeyHalfPressed += (o, arg) => photoCamera.Focus();
            }

            if (timer == null)
            {
                timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
                if (photoCamera.IsFocusSupported)
                {
                    photoCamera.AutoFocusCompleted += (o, arg) => { if (arg.Succeeded) ScanPreviewBuffer(); };
                    timer.Tick += (o, arg) => { try { photoCamera.Focus(); } catch (Exception) { } };
                }
                else
                {
                    timer.Tick += (o, arg) => ScanPreviewBuffer();
                }
            }

            previewRect.Visibility = System.Windows.Visibility.Visible;
            timer.Start();
        }

        private void ScanPreviewBuffer()
        {
            if (luminance == null)
                return;

            photoCamera.GetPreviewBufferY(luminance.PreviewBufferY);
            // use a dummy writeable bitmap because the luminance values are written directly to the luminance buffer
            var result = reader.Decode(dummyBitmap);

            if (result != null)
                viewModel.Scanned(result.Text);
        }

        private void OnPhotoCameraInitialized(object sender, CameraOperationCompletedEventArgs e)
        {
            var width = Convert.ToInt32(photoCamera.PreviewResolution.Width);
            var height = Convert.ToInt32(photoCamera.PreviewResolution.Height);

            Dispatcher.BeginInvoke(() =>
            {
                previewTransform.Rotation = photoCamera.Orientation;
                // create a luminance source which gets its values directly from the camera
                // the instance is returned directly to the reader
                luminance = new PhotoCameraLuminanceSource(width, height);
                reader = new BarcodeReader(null, bmp => luminance, null);
            });
        }
    }
}