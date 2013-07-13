using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using StreetPay.Library;
using ZXing;
using System.Windows;

namespace StreetPayWP.ViewModels
{
    public class ScanVM : VMBase
    {

        public ScanVM()
        {
        }



        private int GetIdFrom(string text)
        {
            var slashIndex = text.LastIndexOf('/');
            if (slashIndex == -1 || slashIndex == text.Length - 1)
                return -1;
            int num = -1;
            if (int.TryParse(text.Substring(slashIndex + 1), out num))
                return num;
            else
                return -1;
        }

        internal void Scanned(string text)
        {
            var id = GetIdFrom(text);

            if (id == -1)
            {
                ShowMessage("The code is invalid");
                return;
            }


            IsLoading = true;
            BarText = "descargando proyecto...";

            new StreetPayService().GetProject(id).ContinueWith((t) =>
            {
                var r = t.Result;
                IsLoading = false;

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
}
