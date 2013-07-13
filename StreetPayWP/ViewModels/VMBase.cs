using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StreetPayWP.ViewModels
{
    public class VMBase : ViewModelBase
    {
        protected NavigationService Navigator = new NavigationService();

        protected void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        protected void ShowMessage(string message, params object[] args)
        {
            ShowMessage(String.Format(message, args));
        }
    }
}
