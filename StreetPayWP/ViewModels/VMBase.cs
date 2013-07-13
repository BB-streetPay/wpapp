using GalaSoft.MvvmLight;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StreetPayWP.ViewModels
{
    [ImplementPropertyChanged]
    public class VMBase : ViewModelBase
    {
        public bool IsLoading { get; set; }
        public string BarText { get; set; }

        protected NavigationService Navigator = new NavigationService();

        protected void ShowMessage(string message)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(message));
        }

        protected void ShowMessage(string message, params object[] args)
        {
            ShowMessage(String.Format(message, args));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => RaisePropertyChanged(propertyName));
        }
    }
}
