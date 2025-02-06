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
using Barroc_intens.Models;

namespace Barroc_intens.Pages;

public sealed partial class NavigationHeader : UserControl
{
    public Frame ParentFrame { get; set; }

    public NavigationHeader()
    {
        InitializeComponent();
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        if (App.MainRootFrame != null)
        {
            App.MainRootFrame.Navigate(typeof(LoginPage));
            User.LoggedInUser = null;
        }
    }
}