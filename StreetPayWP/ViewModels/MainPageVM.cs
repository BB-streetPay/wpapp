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
using System.Diagnostics;

namespace StreetPayWP.ViewModels
{
    [ImplementPropertyChanged]
    public class MainPageVM : VMBase
    {
        public SafeObservable<StreetPay.Library.Project> Projects { get; set; }
        public ICommand AddProject { get; set; }
        public ICommand ScanImage { get; set; }
        public StreetPay.Library.Project SelectedProject { get; set; }

        public MainPageVM()
        {
            this.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedProject")
                {
                    ProjectVM.Project = SelectedProject;
                    Navigator.NavigateTo("/Views/Project.xaml");
                }
            };

            AddProject = new RelayCommand(() =>
            {
               
            });

            ScanImage = new RelayCommand(() =>
            {
                Navigator.NavigateTo("/Views/Scan.xaml");
            });

            Projects = new SafeObservable<StreetPay.Library.Project>();
        }

        public async void OnLoad()
        {
            var service = new StreetPayService();
            IsLoading = true;
            var response = await service.GetProjects();
            IsLoading = false;

            if (response == null)
                return;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                ShowMessage("Error cargando proyectos: {0}, {1}", response.StatusCode, response.Content);
                return;
            }

            if (Projects != null)
                Projects.BulkAdd(response.Data);
        }

    }
}
