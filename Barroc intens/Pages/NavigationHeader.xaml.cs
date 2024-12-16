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

namespace Barroc_intens.Pages
{
    public sealed partial class NavigationHeader : UserControl
    {
        public Frame ParentFrame { get; set; }
        public NavigationHeader()
        {
            this.InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.TryGoBack();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            ParentFrame.Navigate(typeof(LoginPage));
            DisplayDialog("Uitgelogd", "U bent uitgelogd");


        }
        private async void DisplayDialog(string message, string title)
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
