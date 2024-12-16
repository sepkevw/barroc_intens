using Barroc_intens.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Navigation;

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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHeader.ParentFrame = Frame;
        }

        private async Task LoadAppointments()
        {
            try
            {
                using var conn = new AppDbContext();
                var appointments = await conn.Appointments
                    .Include(u => u.User)
                    .ToListAsync();

                DashboardListView.ItemsSource = appointments;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading appointments: {ex.Message}");
            }
        }

        private void dashboardListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Appointment clickedAppointment)
            {
                AppointmentDetailView.DataContext = clickedAppointment;
            }
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filterText = FilterTextBox.Text.ToLower();
            using var conn = new AppDbContext();

            if (string.IsNullOrWhiteSpace(filterText))
            {
                DashboardListView.ItemsSource = conn.Appointments.ToList();
            }
            else
            {
                var filteredAppointments = conn.Appointments
                    .Include(u => u.User)
                    .ToList()
                    .Where(a =>
                        (a.Description != null && a.Description.ToLower().Contains(filterText)) ||
                        (a.Date.ToString("yyyy-MM-dd").Contains(filterText)) ||
                        (a.Location != null && a.Location.ToLower().Contains(filterText)) || 
                        (a.Duration.ToString().Contains(filterText)) )
                    .ToList();

                DashboardListView.ItemsSource = filteredAppointments;
            }
        }
    }
}
