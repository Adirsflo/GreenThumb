using System.Windows;

namespace GreenThumb.Windows
{
    /// <summary>
    /// Interaction logic for PlantDetailsWindow.xaml
    /// </summary>
    public partial class PlantDetailsWindow : Window
    {
        public PlantDetailsWindow()
        {
            InitializeComponent();
        }
        private void blkInformation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Information about this window till be showed here", "Information");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PlantWindow plantWindow = new PlantWindow();
            plantWindow.Show();
            Close();
        }

        private void lstAddInstruction_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            MessageBox.Show("Instruction added!", "Instruction added");
        }

        private void rbAddedToGardenTrue_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Added to garden");
        }

        private void rbAddedToGardenFalse_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Removed from garden");
        }
    }
}
