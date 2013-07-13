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

namespace StreetPayWP
{
    public partial class Project : PhoneApplicationPage
    {
        ProjectVM viewModel = new ProjectVM();
        public Project()
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void Border_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            viewModel.PayProject.Execute(null);
        }
    }
}