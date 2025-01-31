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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Barroc_intens.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();

            string Title = "test";
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
            DisplayDialog("hoihoi", "Welcome");
        }
        private async void DisplayDialog( string message, string title)
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                XamlRoot = this.XamlRoot,
                Title = title,
                Content = message,
                CloseButtonText = "Ok"
            };

            await noWifiDialog.ShowAsync();
        }
    }
}
