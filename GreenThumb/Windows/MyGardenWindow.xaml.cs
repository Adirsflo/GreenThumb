using System.Windows;

namespace GreenThumb.Windows
{
    /// <summary>
    /// Interaction logic for MyGardenWindow.xaml
    /// </summary>
    public partial class MyGardenWindow : Window
    {
        public MyGardenWindow()
        {
            InitializeComponent();
        }

        private void blkInformation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Information about how to use the application will be displayed here!", "Information");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PlantWindow plantWindow = new();
            plantWindow.Show();
            Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Searching");
        }
    }
}
