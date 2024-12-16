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
    public sealed partial class EditProductPage : Page
    {
        private int selectedProductId;
        public EditProductPage()
        {
            this.InitializeComponent();

            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Product product)
            {
                selectedProductId = product.Id;    
                ProductNameTb.Text = product.Name;
                ProdnumberTb.Text = product.ProductNumber.ToString();
                int categoryCb = product.CategoryId;
                if (categoryCb == 1)
                {
                    ComboBoxCb.SelectedItem = "Automaten";
                }
                else if (categoryCb == 2) 
                {
                    ComboBoxCb.SelectedItem = "Koffie Bonen";
                }
                UnitsInStockTb.Text = product.UnitsInStock.ToString();
                InstallCostTb.Text = product.InstallCost.ToString();
                LeaseCostTb.Text = product.LeaseCost.ToString();
                PricePerKiloTb.Text = product.PricePerKilo.ToString();

            }
        }


        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductNameTb.Text != null && ProdnumberTb.Text != null && UnitsInStockTb.Text != null && LeaseCostTb.Text != null && InstallCostTb.Text != null && PricePerKiloTb.Text != null)
            {
                string productName = ProductNameTb.Text;
                int productNumber = Convert.ToInt32(ProdnumberTb.Text);
                int unitsInStock = Convert.ToInt32(UnitsInStockTb.Text);
                double leaseCost = Convert.ToDouble(LeaseCostTb.Text);
                double installCost = Convert.ToDouble(InstallCostTb.Text);
                double pricePerKilo = Convert.ToDouble(PricePerKiloTb.Text);

                using (var connection = new AppDbContext())
                {
                    var product = connection.Products.Find(selectedProductId); 

                    if (product != null)
                    {
                        product.Name = productName;
                        product.ProductNumber = productNumber;
                        product.UnitsInStock = unitsInStock;
                        if (LeaseCostTb.Text.Length < 1)
                        {
                            leaseCost = 0;  
                        }    
                        product.LeaseCost = leaseCost;
                        if (InstallCostTb.Text.Length < 1)
                        {
                            installCost = 0;
                        }
                        product.InstallCost = installCost;
                        if (PricePerKiloTb.Text.Length < 0)
                        {
                            pricePerKilo = 0;
                        }
                        product.PricePerKilo = pricePerKilo;
                      
                        connection.SaveChanges();
                    }
                    

                    var dialog = new ContentDialog
                    {
                        Title = "Voor elkaar!",
                        Content = "Wijzigingen aan " + productName + " zijn opgeslagen",
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

            Frame.Navigate(typeof(PurchasingDashboardPage));
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PurchasingDashboardPage));
        }
    }
}
