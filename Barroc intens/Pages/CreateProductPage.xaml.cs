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

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            //velden legen
            Frame.Navigate(typeof(PurchasingDashboardPage));
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductNameTb.Text != null && ProdnumberTb.Text != null && UnitsInStockTb.Text != null && LeaseCostTb.Text != null && InstallCostTb.Text != null && PricePerKiloTb.Text != null && ComboBoxCb.SelectedItem != null)
            {
                string productName = ProductNameTb.Text;
                int productNumber = Convert.ToInt32(ProdnumberTb.Text);
                int unitsInStock = Convert.ToInt32(UnitsInStockTb.Text);
                double leaseCost = Convert.ToDouble(LeaseCostTb.Text);
                double installCost = Convert.ToDouble(InstallCostTb.Text);
                double pricePerKilo = Convert.ToDouble(PricePerKiloTb.Text);
                
                var selectedCategory = ComboBoxCb.SelectionBoxItem.ToString();
                int selectedCategoryToInt = 0;

                if (selectedCategory == "Koffiebonen")
                {
                    selectedCategoryToInt = 2;
                }
                if (selectedCategory == "Automaat")
                {
                    selectedCategoryToInt = 1;
                }

                using (var connection = new AppDbContext())
                {

                    Product newProduct = new()
                    {
                        Name = productName,
                        ProductNumber = productNumber,
                        UnitsInStock = unitsInStock,
                        InstallCost = installCost,
                        LeaseCost = leaseCost,
                        PricePerKilo = pricePerKilo,
                        CategoryId = selectedCategoryToInt,
                    };

                    connection.Products.Add(newProduct);    
                    connection.SaveChanges();

                    var dialog = new ContentDialog
                    {
                        Title = "Voor elkaar!",
                        Content = productName + " is opgeslagen",
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

