using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace Barroc_intens.Pages
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void FinanceButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FinanceDashboardPage));
        }

        private void MaintenanceButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MaintenanceDashboardPage));
        }

        private void PurchasingButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PurchasingDashboardPage));
        }

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SalesDashboardPage));
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using var conn = new AppDbContext();
            var dbUser = conn.Users.FirstOrDefault(u => u.Username == UsernameInput.Text);

            
            if(dbUser == null)
            {
                ErrorMessage.Text = "Password or Username is wrong.";
            } else
            {
                var verifyLogin = SecureHasher.Verify(PasswordInput.Text, dbUser.Password);
                if(verifyLogin)
                {
                    switch (dbUser.RoleId)
                    {
                        case 7:
                        case 8:
                            //inkoop / purchasing dept.
                            Frame.Navigate(typeof(PurchasingDashboardPage));
                            break;
                        case 3:
                        case 4:
                            //financien / finance 
                            Frame.Navigate(typeof(FinanceDashboardPage));
                            break;
                        case 10:
                        case 11:
                        case 12:
                            //onderhoud / maintenance
                            Frame.Navigate(typeof(MaintenanceDashboardPage));
                            break;
                        case 5:
                        case 6:
                            //verkoop / sales
                            Frame.Navigate(typeof(SalesDashboardPage));
                            break;
                        default:
                            Frame.Navigate(typeof(LoginPage));
                            break;
                    }
                } else
                {
                    ErrorMessage.Text = "Password or Username is wrong.";
                }
            }
        }
    }
}
