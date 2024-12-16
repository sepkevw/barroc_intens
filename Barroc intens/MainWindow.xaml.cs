using Barroc_intens.Pages;
using Microsoft.UI.Xaml;
using System.Linq;

namespace Barroc_intens
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            using var connection = new AppDbContext();
            connection.Database.EnsureDeleted();
            connection.Database.EnsureCreated();
            
            contentFrame.Navigate(typeof(LoginPage));
        }

    }
}
