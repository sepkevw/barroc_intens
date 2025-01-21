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
            string ProdNumberAsString = ProdnumberTb.Text;
            
            bool ProdNumberIsNumerical = int.TryParse(ProdNumberAsString, out _);

            string UnitsInStockAsString = UnitsInStockTb.Text;
            bool UnitsInStockIsNumerical = int.TryParse(UnitsInStockAsString, out _); ;

            string LeaseCostAsString = LeaseCostTb.Text;
            bool LeaseCostIsNumerical = int.TryParse(LeaseCostAsString, out _); ;
            ;

            string InstallCostAsString = InstallCostTb.Text;
            bool InstallCostIsNumerical = int.TryParse(InstallCostAsString, out _); ;


            string PricePerKiloAsString = PricePerKiloTb.Text;
            bool PricePerKiloIsNumerical = int.TryParse(PricePerKiloAsString, out _); ;



            if (ProductNameTb.Text.Length >= 0 && ProdnumberTb.Text.Length >= 0 && UnitsInStockTb.Text.Length >= 0 && LeaseCostTb.Text != null && InstallCostTb.Text.Length >= 0 && PricePerKiloTb.Text.Length >= 0 && ComboBoxCb.SelectedItem != null && ProdNumberIsNumerical == true && UnitsInStockIsNumerical == true && LeaseCostIsNumerical == true && InstallCostIsNumerical == true && PricePerKiloIsNumerical == true)
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

                    var Dialog = new ContentDialog
                    {
                        Title = "Voor elkaar!",
                        Content = ProductName + " is opgeslagen",
                        CloseButtonText = "Sluit",
                        XamlRoot = this.Content.XamlRoot
                    };

                    Frame.Navigate(typeof(PurchasingDashboardPage));

                    await Dialog.ShowAsync();
                }
            }
            else
            {
                var Dialog = new ContentDialog
                {
                    Title = "Waarschuwing:",
                    Content = "Een or meerdere velden is leeggelaten of verkeerd ingevuld!",
                    CloseButtonText = "Sluit",
                    XamlRoot = this.Content.XamlRoot
                };

                await Dialog.ShowAsync();
            }
        }
    }
}

