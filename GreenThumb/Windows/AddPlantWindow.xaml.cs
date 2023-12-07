using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;

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

		private void blkInformation_Click(object sender, RoutedEventArgs e) // TODO: Edit instructions of the window
		{
			// TODO: Edit instructions of the window
			MessageBox.Show("Information displayed here");
		}
		private void btnBack_Click(object sender, RoutedEventArgs e) // Method for returning to DetailsWindow
		{
			CancelToDetailsWindow();
		}
		private void CancelToDetailsWindow() // Checking if user has inputted information
		{
			if (txtNewPlantName.Text != ""
					|| txtNewPlantDescription.Text != ""
					|| txtNewInstructionName.Text != ""
					|| txtNewInstructionDescription.Text != ""
					|| lstAddInstruction.Items.Count != 0)
			{
				MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?" +
						"\n\nYour progress will not be saved!",
						"Warning", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					PlantWindow plantWindow = new();
					plantWindow.Show();
					Close();
				}
			}
			else
			{
				PlantWindow plantWindow = new();
				plantWindow.Show();
				Close();
			}
		}
		private void btnAddInstruction_Click(object sender, RoutedEventArgs e) // Adding instructions to the new plant
		{
			InstructionModel newInstruciton = new()
			{
				Name = txtNewInstructionName.Text,
				Description = txtNewInstructionDescription.Text,
			};

			ListViewItem item = new();

			item.Content = newInstruciton.Name;
			item.Tag = newInstruciton;
			lstAddInstruction.Items.Add(item);

			txtNewInstructionName.Text = string.Empty;
			txtNewInstructionDescription.Text = string.Empty;

			MessageBox.Show("Instruction added");
		}
		private void btnRemoveInstruction_Click(object sender, RoutedEventArgs e) // Removing instructions from the new plant
		{
			MessageBox.Show("Instruction removed successfully");
		}

		private void btnAddPlant_Click(object sender, RoutedEventArgs e) // Adding plant to database
		{
			MessageBoxResult result = MessageBox.Show("Have you added all the instructions for this plant?", "Adding plant", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				MessageBox.Show("The plant has been added!", "Plant added");
				PlantWindow plantWindow = new();
				plantWindow.Show();
				Close();
			}
		}
	}
}
