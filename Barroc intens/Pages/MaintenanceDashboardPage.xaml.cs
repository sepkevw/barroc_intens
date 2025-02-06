using Barroc_intens.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Barroc_intens.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

        private async void RefreshAppointmentsList()
        {
            await LoadAppointments();
        }

        private async void AddAppointmentButton_OnClick(object sender, RoutedEventArgs e)
        {
            await showAddApointmentPopupAsync();
        }

        private async Task showAddApointmentPopupAsync()
        {
            var dialog = new ContentDialog
            {
                Title = "Add new appointment",
                PrimaryButtonText = "Save",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };

            var context = new AppDbContext();
            var workers = context.Users.Where(u => u.RoleId == 10 || u.RoleId == 11).ToList();
            var customers = context.Customers.ToList();

            var stackPanel = new StackPanel
            {
                Spacing = 15,
                Padding = new Thickness(20)
            };

            var datePickerBox = new DatePicker
            {
                Header = "Vul Datum in",
                Margin = new Thickness(0, 10, 0, 0)
            };

            var descriptionTextBox = new TextBox
            {
                Header = "Beschrijving",
                PlaceholderText = "Vul beschrijving in",
                AcceptsReturn = true,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 10, 0, 0)
            };

            var durationNumberBox = new NumberBox
            {
                Header = "Tijdsduur",
                PlaceholderText = "Vul de tijdsduur in",
                Margin = new Thickness(0, 10, 0, 0)
            };

            var assignedUserBox = new ComboBox
            {
                Header = "Toegewezen werknemer",
                PlaceholderText = "Kies een werknemer",
                ItemsSource = workers,
                DisplayMemberPath = "Username",
                Margin = new Thickness(0, 10, 0, 0),
                Width = 300,
            };

            var customerComboBox = new ComboBox
            {
                Header = "Afspraak voor klant",
                PlaceholderText = "Kies een klant",
                ItemsSource = customers,
                DisplayMemberPath = "CompanyName",
                Margin = new Thickness(0, 10, 0, 0),
                Width = 300,
            };

            var locationTextBox = new TextBox
            {
                Header = "Locatie",
                PlaceholderText = "Voer locatie in (bijv. adres, plaatsnaam)",
                Width = 300
            };

            stackPanel.Children.Add(datePickerBox);
            stackPanel.Children.Add(locationTextBox);
            stackPanel.Children.Add(descriptionTextBox);
            stackPanel.Children.Add(durationNumberBox);
            stackPanel.Children.Add(assignedUserBox);
            stackPanel.Children.Add(customerComboBox);

            dialog.Content = stackPanel;
            dialog.XamlRoot = this.XamlRoot;


            dialog.Content = stackPanel;
            dialog.XamlRoot = this.XamlRoot;

            var result = await dialog.ShowAsync();

            // Kijkt of alles is ingevuld.
            if (result == ContentDialogResult.Primary)
            {
                if (assignedUserBox.SelectedItem == null ||
                    customerComboBox.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(locationTextBox.Text) ||
                    string.IsNullOrWhiteSpace(descriptionTextBox.Text) ||
                    string.IsNullOrWhiteSpace(durationNumberBox.Text))
                {
                    // content dialog voor als er niks in ingevuld bij een input regel.
                    await new ContentDialog
                    {
                        Title = "Error",
                        Content = "Please fill in all fields correctly.",
                        CloseButtonText = "OK",
                        DefaultButton = ContentDialogButton.Close,
                        XamlRoot = this.XamlRoot
                    }.ShowAsync();
                    return;
                }

                try
                {
                    var selectedUser = (User)assignedUserBox.SelectedItem;
                    var selectedCustomer = (Customer)customerComboBox.SelectedItem;
                    var appointment = new Appointment
                    {
                        Date = datePickerBox.Date.DateTime,
                        Duration = int.Parse(durationNumberBox.Text),
                        Location = locationTextBox.Text,
                        UserId = selectedUser.Id,
                        Description = descriptionTextBox.Text
                    };

                    var customerAppointment = new CustomerAppointment
                    {
                        CustomerId = selectedCustomer.Id,
                        Appointment = appointment
                    };

                    context.CustomerAppointments.Add(customerAppointment);
                    await context.SaveChangesAsync();

                    await new ContentDialog
                    {
                        Title = "Success",
                        Content = "The appointment has been saved successfully.",
                        CloseButtonText = "OK",
                        DefaultButton = ContentDialogButton.Close,
                        XamlRoot = this.XamlRoot
                    }.ShowAsync();

                    RefreshAppointmentsList();
                }
                catch (Exception ex)
                {
                    await new ContentDialog
                    {
                        Title = "Error",
                        Content = $"An error occurred while saving the appointment: {ex.Message}",
                        CloseButtonText = "OK",
                        DefaultButton = ContentDialogButton.Close,
                        XamlRoot = this.XamlRoot
                    }.ShowAsync();
                }
            }
        }
    }
}
