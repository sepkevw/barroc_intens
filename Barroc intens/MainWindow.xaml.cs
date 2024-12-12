using Barroc_intens.Pages;
using Microsoft.UI.Xaml;
using System.Linq;

namespace Barroc_intens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            using var connection = new AppDbContext();
            connection.Database.EnsureDeleted();
            connection.Database.EnsureCreated();

            contentFrame.Navigate(typeof(LoginPage));
            
            // Niet nodig, verwijder voor oplevering. - Ruchan
            //var rootFrame = new Frame();
            //this.Content = rootFrame;
            ////rootFrame.Navigate(typeof(LoginPage));

            //int RandomId = new Random().Next(1, 25);
            //using(var db = new AppDbContext())  
            //{
            //    User LoggedInUser = db.Users.First(u => u.Id == RandomId);
            //    //User LoggedInUser = new User
            //    //{
            //    //    Id = 1,
            //    //    Username = "Collin",
            //    //    RoleId = 1, // Assuming there are 3 roles
            //    //    Created_at = DateTime.Now,
            //    //    Role = null
            //    //}; Test User
            //    rootFrame.Navigate(typeof(LoginPage));
            //}
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
