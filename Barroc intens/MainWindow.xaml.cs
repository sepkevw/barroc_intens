using Barroc_intens.Pages;
using Microsoft.UI.Xaml;

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
        }
    }
}
