using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Barroc_intens.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace Barroc_intens.Pages;

public sealed partial class LoginPage : Page
{
    public LoginPage()
    {
        InitializeComponent();
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
        //DisplayDialog("hoihoi", "Welcome");
    }

    //private async void DisplayDialog(string message, string title)
    //{
    //    ContentDialog noWifiDialog = new ContentDialog()
    //    {
    //        XamlRoot = this.XamlRoot,
    //        Title = title,
    //        Content = message,
    //        CloseButtonText = "Ok"
    //    };

    //    await noWifiDialog.ShowAsync();
    //}

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var conn = new AppDbContext();
        var dbUser = conn.Users.FirstOrDefault(u => u.Username == UsernameInput.Text);


        if (dbUser == null)
        {
            ErrorMessage.Text = "Password or Username is wrong.";
        }
        else
        {
            var verifyLogin = SecureHasher.Verify(PasswordInput.Password, dbUser.Password);
            if (verifyLogin)
            {
                dbUser.RememberToken = User.GenerateRememberToken();

                var storageFolder = ApplicationData.Current.LocalFolder;
                var cookieFile = await storageFolder.CreateFileAsync(
                    "remember_token.txt",
                    CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(cookieFile, dbUser.RememberToken);
                conn.SaveChanges();

                User.LoggedInUser = dbUser;

                Frame.Navigate(typeof(NavigationTabPage));
            }
        }
    }
}