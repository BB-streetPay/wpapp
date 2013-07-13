using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Phone.Tasks;
using ZXing;
using System.Windows.Media.Imaging;
using StreetPay.Library;

namespace StreetPayWP.ViewModels
{
    public class ScanVM : VMBase
    {
        public ICommand Scan { get; set; }

        public ScanVM()
        {
            Scan = new RelayCommand(() =>
            {
                var task = new CameraCaptureTask();
                task.Completed += PhotoCaptured;
                task.Show();
            });
        }

        private void PhotoCaptured(object sender, PhotoResult e)
        {
            if (e.TaskResult != TaskResult.OK)
                return;

            var reader = new BarcodeReader();
            var bitmap = new BitmapImage();
            bitmap.SetSource(e.ChosenPhoto);
            var wb = new WriteableBitmap(bitmap);
            var result = reader.Decode(wb);

            if (result == null)
            {
                ShowMessage("We couldn't detect a barcode there.");
            }
            else
            {
                var text = result.Text;
                var id = GetIdFrom(text);

                if (id == -1)
                {
                    ShowMessage("The code is invalid");
                    return;
                }

                new StreetPayService().GetProject(id).ContinueWith((t) =>
                {
                    var r = t.Result;
                    if (r.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        ShowMessage("Error downloading the project");
                    }
                    else
                    {
                        ProjectVM.Project = r.Data;
                        Navigator.NavigateTo("Pages/Project.xaml");
                    }
                });
            }
        }

        private int GetIdFrom(string text)
        {
            var slashIndex = text.LastIndexOf('/');
            if (slashIndex == -1 || slashIndex == text.Length - 1)
                return -1;

            return int.Parse(text.Substring(slashIndex + 1));
        }
    }
}
