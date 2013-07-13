using GalaSoft.MvvmLight.Command;
using PropertyChanged;
using StreetPay.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StreetPayWP.ViewModels
{
    [ImplementPropertyChanged]
    public class AddProjectVM : VMBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICommand Send { get; set; }

        public AddProjectVM()
        {
            Send = new RelayCommand(() =>
            {
                var service = new StreetPayService();
                IsLoading = true;
                service.AddProject(new StreetPay.Library.Project
                {
                    Name = Name,
                    Description = Description
                }).ContinueWith((t) => {
                    IsLoading = false;
                    ShowMessage("Proyecto añadido");
                    Navigator.GoBack();
                });

                    
            });
        }

    }
}
