using System.Windows;

namespace GreenThumb.Windows
{
    /// <summary>
    /// Interaction logic for AddPlantWindow.xaml
    /// </summary>
    public partial class AddPlantWindow : Window
    {
        public AddPlantWindow()
        {
            InitializeComponent();
        }
        private void blkInformation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Information displayed here");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PlantWindow plantWindow = new();
            plantWindow.Show();
            Close();
        }

        private void btnAddPlant_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Plant added (Make sure all instruction is also added first)", "Sure", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Added!");
                PlantWindow plantWindow = new();
                plantWindow.Show();
                Close();
            }
        }

        private void btnAddInstruction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Instruction added");
        }

        private void btnRemoveInstruction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Instruction removed");
        }
    }

}
