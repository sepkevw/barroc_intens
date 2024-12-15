using Barroc_intens.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Barroc_intens.Pages
{
    public sealed partial class MaintenanceDashboardPage : Page
    {
        public MaintenanceDashboardPage()
        {
            this.InitializeComponent();
            Loaded += MaintenanceDashboardPage_Loaded;
        }

        private async void MaintenanceDashboardPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadAppointments();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private async Task LoadAppointments()
        {
            try
            {
                using var conn = new AppDbContext();
                var appointments = await conn.Appointments.ToListAsync();

                dashboardListView.ItemsSource = appointments;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading appointments: {ex.Message}");
            }
        }

        private void dashboardListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedAppointment = e.ClickedItem as Appointment;
            if(selectedAppointment != null)
            {
                var appointmentDialog = new ContentDialog
                {
                    
                };
            }
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filterText = FilterTextBox.Text.ToLower();
            using var conn = new AppDbContext();

            if (string.IsNullOrWhiteSpace(filterText))
            {
                // If no filter, reset to original data
                dashboardListView.ItemsSource = conn.Appointments.ToList();
            }
            else
            {
                // Apply filtering on Name (Description) and Date
                var filteredAppointments = conn.Appointments.ToList()
                    .Where(a =>
                        (a.Description != null && a.Description.ToLower().Contains(filterText)) ||
                        (a.Date != null && a.Date.ToString("yyyy-MM-dd").Contains(filterText)) ||
                        (a.Location != null && a.Location.ToLower().Contains(filterText)) || 
                        (a.Duration != null && a.Duration.ToString().Contains(filterText)) )
                    .ToList();

                dashboardListView.ItemsSource = filteredAppointments;
            }
        }
    }
}
