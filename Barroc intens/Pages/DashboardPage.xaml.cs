using Barroc_intens.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Barroc_intens.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashboardPage : Page
    {
        public DashboardPage()
        {
            this.InitializeComponent();


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            User currentUser = (User)e.Parameter;

            switch (currentUser.RoleId)
            {
                case 1: //inkoop / purchasing dept.
                    using (var db = new AppDbContext())
                    {
                        dashboardListView.ItemsSource = db.Users.ToList();
                        dashboardGridView.ItemsSource = db.Users.ToList();
                    }
                break;
                case 2: //financien / finance 
                    using (var db = new AppDbContext())
                    {
                        dashboardListView.ItemsSource = db.Users.ToList();
                        dashboardGridView.ItemsSource = db.Users.ToList();
                    }
                break;
                case 3: //onderhoud / maintenance
                    using (var db = new AppDbContext())
                    {
                        dashboardListView.ItemsSource = db.Users.ToList();
                        dashboardGridView.ItemsSource = db.Users.ToList();
                    }
                break;
                case 4: //verkoop / sales
                    using (var db = new AppDbContext())
                    {
                        dashboardListView.ItemsSource = db.Users.ToList();
                        dashboardGridView.ItemsSource = db.Users.ToList();
                    }
                break;
            }
        }
    }
}
