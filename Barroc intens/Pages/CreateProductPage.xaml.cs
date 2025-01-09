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
using Barroc_intens.Models;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Barroc_intens.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateProductPage : Page
    {
        public CreateProductPage()
        {
            this.InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //velden legen
            Frame.Navigate(typeof(PurchasingDashboardPage));
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductNameTb.Text != null && ProdnumberTb.Text != null && UnitsInStockTb.Text != null && LeaseCostTb.Text != null && InstallCostTb.Text != null && PricePerKiloTb.Text != null && ComboBoxCb.SelectedItem != null)
            {
                string ProductName = ProductNameTb.Text;
                int ProductNumber = Convert.ToInt32(ProdnumberTb.Text);
                int UnitsInStock = Convert.ToInt32(UnitsInStockTb.Text);
                double LeaseCost = Convert.ToDouble(LeaseCostTb.Text);
                double InstallCost = Convert.ToDouble(InstallCostTb.Text);
                double PricePerKilo = Convert.ToDouble(PricePerKiloTb.Text);
                
                var SelectedCategory = ComboBoxCb.SelectionBoxItem.ToString();
                int SelectedCategoryToInt = 0;

                if (SelectedCategory == "Koffiebonen")
                {
                    SelectedCategoryToInt = 2;
                }
                if (SelectedCategory == "Automaat")
                {
                    SelectedCategoryToInt = 1;
                }

                using (var connection = new AppDbContext())
                {

                    Product newProduct = new()
                    {
                        Name = ProductName,
                        ProductNumber = ProductNumber,
                        UnitsInStock = UnitsInStock,
                        InstallCost = InstallCost,
                        LeaseCost = LeaseCost,
                        PricePerKilo = PricePerKilo,
                        CategoryId = SelectedCategoryToInt,
                    };

                    connection.Products.Add(newProduct);    
                    connection.SaveChanges();

                    var dialog = new ContentDialog
                    {
                        Title = "Voor elkaar!",
                        Content = ProductName + " is opgeslagen",
                        CloseButtonText = "Sluit",
                        XamlRoot = this.Content.XamlRoot
                    };

                    Frame.Navigate(typeof(PurchasingDashboardPage));

                    await dialog.ShowAsync();
                }
            }
            else
            {
                var dialog = new ContentDialog
                {
                    Title = "Waarschuwing:",
                    Content = "Een or meerdere velden is leeggelaten!",
                    CloseButtonText = "Sluit",
                    XamlRoot = this.Content.XamlRoot
                };

                await dialog.ShowAsync();
            }
        }
    }
}

