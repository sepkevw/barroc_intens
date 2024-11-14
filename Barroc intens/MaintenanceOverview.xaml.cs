using Barroc_intens.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Controls;

using System.Linq;

namespace Barroc_intens
{
    public sealed partial class MaintenanceOverview : Page
    {
        public MaintenanceOverview()
        {
            this.InitializeComponent();
            using var connection = new AppDbContext();

            AppointmentsListView.ItemsSource = connection.Appointments
                .Include(a => a.Customer)
                .ToList();
        }

        private void AppointmentsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Appointment selectedAppointment = (Appointment)e.ClickedItem;

            appNameTextBlock.Text = selectedAppointment.Customer.CompanyName;
            appDescriptionTextBlock.Text = selectedAppointment.Description;
            appLocationTextBlock.Text = selectedAppointment.Location;
            appDateTextBlock.Text = selectedAppointment.Date.ToString();
            ExtraInfoTxt.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        }
    }
}
