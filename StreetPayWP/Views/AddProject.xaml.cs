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

namespace StreetPayWP.Views
{
    public partial class AddProject : PhoneApplicationPage
    {
        public AddProject()
        {
            this.DataContext = new AddProjectVM();
            InitializeComponent();
        }
    }
}