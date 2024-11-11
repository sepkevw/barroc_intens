using Microsoft.UI.Xaml;

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
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }
    }
}
