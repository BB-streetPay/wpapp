using GalaSoft.MvvmLight;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreetPay;
using StreetPay.Library;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace StreetPayWP.ViewModels
{
    [ImplementPropertyChanged]
    public class MainPageVM : VMBase
    {
        public SafeObservable<Project> Projects { get; set; }
        public ICommand AddProject { get; set; }
        public ICommand ScanImage { get; set; }
        public Project SelectedProject { get; set; }

        public MainPageVM()
        {
            AddProject = new RelayCommand(() =>
            {
                ProjectVM.Project = SelectedProject;
                Navigator.NavigateTo("Views/Project.xaml");
            });

            ScanImage = new RelayCommand(() =>
            {
                Navigator.NavigateTo("Views/Scan.xaml");
            });
        }

    }
}
