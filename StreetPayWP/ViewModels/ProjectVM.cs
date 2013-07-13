using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PropertyChanged;
using StreetPay;
using StreetPay.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StreetPayWP.ViewModels
{
    [ImplementPropertyChanged]
    public class ProjectVM : VMBase
    {
        public static Project Project { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public SafeObservable<Payment> Payments { get; set; }
        public int TotalBacked { get; set; }
        public ICommand PayProject { get; set; }
        public int MoneyToPay { get; set; }

        public ProjectVM()
        {
            PayProject = new RelayCommand(() => 
            {
                var service = new StreetPayService();
                var task = service.MakePayment(Project, MoneyToPay);
                task.ContinueWith((t) =>
                {
                    var result = t.Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        ShowMessage("Has pagado {0} € para este proyecto.", MoneyToPay);
                    else
                        ShowMessage("Ha ocurrido un error {0}, {1}", result.StatusCode, result.Content);
                });
            });
        }
    }
}
