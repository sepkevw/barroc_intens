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
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Barroc_intens.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Barroc_intens.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class NavigationTabPage : Page
{
    public NavigationTabPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        SeePermissions();
    }

    public async void SeePermissions()
    {
        var isUserLoaded = await User.TryLoadUser();
        if (isUserLoaded) LoadTabs(User.LoggedInUser.RoleId);
    }

    private void LoadTabs(int roleId)
    {
        var availableTabs = new List<TabViewItem>();

        switch (roleId)
        {
            case 1: // Admin
            case 2: // CEO
                availableTabs.Add(CreateTab("Finance", typeof(FinanceDashboardPage)));
                availableTabs.Add(CreateTab("Login", typeof(LoginPage)));
                availableTabs.Add(CreateTab("Maintenance", typeof(MaintenanceDashboardPage)));
                availableTabs.Add(CreateTab("Products", typeof(ProductsPage)));
                availableTabs.Add(CreateTab("Purchasing", typeof(PurchasingDashboardPage)));
                availableTabs.Add(CreateTab("Sales", typeof(SalesDashboardPage)));
                break;

            case 3: // HeadFinance
                availableTabs.Add(CreateTab("Finance", typeof(FinanceDashboardPage)));
                availableTabs.Add(CreateTab("Sales", typeof(SalesDashboardPage)));
                availableTabs.Add(CreateTab("Purchasing", typeof(PurchasingDashboardPage)));
                break;

            case 4: // AdminFinance
                availableTabs.Add(CreateTab("Finance", typeof(FinanceDashboardPage)));
                break;

            case 5: // HeadSales
                availableTabs.Add(CreateTab("Sales", typeof(SalesDashboardPage)));
                availableTabs.Add(CreateTab("Finance", typeof(FinanceDashboardPage)));
                break;

            case 6: // Consultant
                availableTabs.Add(CreateTab("Sales", typeof(SalesDashboardPage)));
                break;

            case 7: // HeadInkoop
                availableTabs.Add(CreateTab("Purchasing", typeof(PurchasingDashboardPage)));
                availableTabs.Add(CreateTab("Finance", typeof(FinanceDashboardPage)));
                break;

            case 8: // Inkoper
                availableTabs.Add(CreateTab("Purchasing", typeof(PurchasingDashboardPage)));
                break;

            case 9: // MedewerkerMagazijn
                availableTabs.Add(CreateTab("Products", typeof(ProductsPage)));
                break;

            case 10: // HeadMaintenance
            case 11: // TechnicalService
                availableTabs.Add(CreateTab("Maintenance", typeof(MaintenanceDashboardPage)));
                break;

            case 12: // Planner
                availableTabs.Add(CreateTab("Maintenance", typeof(MaintenanceDashboardPage)));
                availableTabs.Add(CreateTab("Products", typeof(ProductsPage)));
                break;

            default:
                // If no matching role, show only the login page
                availableTabs.Add(CreateTab("Login", typeof(LoginPage)));
                break;
        }

        foreach (var tab in availableTabs) MainTabView.TabItems.Add(tab);
    }

    private TabViewItem CreateTab(string header, Type pageType)
    {
        return new TabViewItem
        {
            Header = header,
            Content = new Frame { SourcePageType = pageType }
        };
    }
}