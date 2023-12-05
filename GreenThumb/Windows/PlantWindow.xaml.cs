using System.Windows;

namespace GreenThumb.Windows
{
    /// <summary>
    /// Interaction logic for PlantWindow.xaml
    /// </summary>
    public partial class PlantWindow : Window
    {
        public PlantWindow()
        {
            InitializeComponent();
        }

        private void blkMyGarden_Click(object sender, RoutedEventArgs e)
        {
            // Open Garden
        }
        private void blkInformation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Information about how to use the application will be displayed here!", "Information");
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            PlantDetailsWindow detailsWindow = new();
            detailsWindow.Show();
            Close();
        }

        private void btnAddPlant_Click(object sender, RoutedEventArgs e)
        {
            AddPlantWindow addWindow = new();
            addWindow.Show();
            Close();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this plant?", "Removing plant", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Plant removed!", "Removing plant");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Searching");
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to sign out?", "Signing out", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Thank you for thinking green!", "Signing out");
                SignInWindow signInWindow = new();
                signInWindow.Show();
                Close();
            }
        }
    }
}
