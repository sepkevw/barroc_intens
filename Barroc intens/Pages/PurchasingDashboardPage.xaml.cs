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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Barroc_intens.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PurchasingDashboardPage : Page
    {
        private static Product SelectedProduct;
        public PurchasingDashboardPage()
        {
            this.InitializeComponent();

            using (var connection = new AppDbContext())
            {
                var Products = connection.Products;

                var LowStock = connection.Products
                               .ToList()
                               .Where(p => p.UnitsInStock <= 50);

                AllStockListView.ItemsSource = Products;
                LowStockGridView.ItemsSource = LowStock;
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void go2CreateViewButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateProductPage));
        }

        private void go2EditViewButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditProductPage), SelectedProduct); 
        }

        private async void deleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct != null)
            {
                var deleteDialog = new ContentDialog
                {
                    Title = "Bevestig Verwijdering",
                    Content = "Verwijderen in permanent en is niet ongedaan te maken (Permanent dus).",
                    PrimaryButtonText = "Verwijder",
                    CloseButtonText = "Annuleer",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.Content.XamlRoot
                };

                var result = await deleteDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    using (var connection = new AppDbContext())
                    {
                        var product2BeRemoved = connection.Products.Single(p => p.Id == selectedProduct.Id);
                        connection.Products.Remove(product2BeRemoved);
                        connection.SaveChanges();

                        var products = connection.Products;

                        var lowStock = connection.Products
                                       .ToList()
                                       .Where(p => p.UnitsInStock <= 50);

                        allStockListView.ItemsSource = products;
                        lowStockGridView.ItemsSource = lowStock;
                    }
                }
                else
                {
                   
                }
            }
        }

        private void lowStockGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            selectedProduct = (Product)e.ClickedItem;

            allStockListView.SelectedItem = null;

            go2EditViewButton.Visibility = Visibility.Visible;
            deleteItemButton.Visibility = Visibility.Visible;
        }

        private void allStockListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            selectedProduct = (Product)e.ClickedItem;

            lowStockGridView.SelectedItem = null;

            go2EditViewButton.Visibility = Visibility.Visible;
            deleteItemButton.Visibility = Visibility.Visible;
        }

        private void Go2CreateViewButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateProductPage));
        }

        private void Go2EditViewButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditProductPage), SelectedProduct); 
        }

        private async void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct != null)
            {
                var DeleteDialog = new ContentDialog
                {
                    Title = "Bevestig Verwijdering",
                    Content = "Verwijderen in permanent en is niet ongedaan te maken (Permanent dus).",
                    PrimaryButtonText = "Verwijder",
                    CloseButtonText = "Annuleer",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.Content.XamlRoot
                };

                var Result = await DeleteDialog.ShowAsync();

                if (Result == ContentDialogResult.Primary)
                {
                    using (var Connection = new AppDbContext())
                    {
                        var Product2BeRemoved = Connection.Products.Single(p => p.Id == SelectedProduct.Id);
                        Connection.Products.Remove(Product2BeRemoved);
                        Connection.SaveChanges();

                        var Products = Connection.Products;

                        var LowStock = Connection.Products
                                       .ToList()
                                       .Where(p => p.UnitsInStock <= 50);

                        AllStockListView.ItemsSource = Products;
                        LowStockGridView.ItemsSource = LowStock;
                    }
                }
            }
        }

        private void LowStockGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SelectedProduct = (Product)e.ClickedItem;

            AllStockListView.SelectedItem = null;

            Go2EditViewButton.Visibility = Visibility.Visible;
            DeleteItemButton.Visibility = Visibility.Visible;
        }

        private void AllStockListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SelectedProduct = (Product)e.ClickedItem;

            LowStockGridView.SelectedItem = null;

            Go2EditViewButton.Visibility = Visibility.Visible;
            DeleteItemButton.Visibility = Visibility.Visible;
        }
    }
}    
