using Barroc_intens.Pages;
using Barroc_intens.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
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

namespace Barroc_intens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private int _previousSelectedIndex = 0;
        public MainWindow()
        {
            this.InitializeComponent();
            using var connection = new AppDbContext();
            connection.Database.EnsureDeleted();
            connection.Database.EnsureCreated();

            int RandomId = new Random().Next(1, 25);
            using(var db = new AppDbContext())  
            {
                User LoggedInUser = db.Users.First(u => u.Id == RandomId);
                //User LoggedInUser = new User
                //{
                //    Id = 1,
                //    Username = "Collin",
                //    RoleId = 1, // Assuming there are 3 roles
                //    Created_at = DateTime.Now,
                //    Role = null
                //}; Test User
                this.contentFrame.Navigate(typeof(LoginPage));
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
