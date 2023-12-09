using GreenThumb.Database;
using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;

namespace GreenThumb.Windows
{
	public partial class AddPlantWindow : Window
	{
		public AddPlantWindow()
		{
			InitializeComponent();
		}

		private void blkInformation_Click(object sender, RoutedEventArgs e) // Information about AddPlantsWindow
		{
			MessageBox.Show("Welcome to the Plant-Adding Section!\n\n" +
										"-You can insert information about the plant on the upper part of the screen\n" +
										"-You need to add atleast one instruction for the plant under \"Instruction Advice\"\n" +
										"-To add an instruction, click on \"Add\"\n" +
										"-If you wish to remove an instruction, click on \"Remove\"\n" +
										"-To view an added instruction in detail, select the instruction under \"Added Instruction\"\n" +
										"-When you are done and ready to add the plant, click on \"Add Plant\"\n" +
										"-On your upper right corner, you can return to dashboard by clicking on \"Back\"", "Information - Navigation");
		}
		private void btnBack_Click(object sender, RoutedEventArgs e) // Method for returning to DetailsWindow
		{
			CancelToDetailsWindow();
		}
		private void CancelToDetailsWindow() // Checking if user has inputted information
		{
			if (!string.IsNullOrWhiteSpace(txtNewPlantName.Text)
					|| !string.IsNullOrWhiteSpace(txtNewPlantDescription.Text)
					|| !string.IsNullOrWhiteSpace(txtNewInstructionName.Text)
					|| !string.IsNullOrWhiteSpace(txtNewInstructionDescription.Text)
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
			if (!string.IsNullOrEmpty(txtNewInstructionName.Text.Trim()) && !string.IsNullOrEmpty(txtNewInstructionDescription.Text.Trim()))
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
			else
			{
				MessageBox.Show("Error: Please fill in the instruction fields", "Warning");
			}
		}
		private void btnRemoveInstruction_Click(object sender, RoutedEventArgs e) // Removing instructions from the new plant
		{
			ListViewItem selectedItem = (ListViewItem)lstAddInstruction.SelectedItem;

			if (selectedItem != null)
			{
				lstAddInstruction.Items.Remove(selectedItem);

				brdInstruction.SetValue(Grid.ColumnSpanProperty, 3);
				txtAddedInstructionDescription.Visibility = Visibility.Hidden;
				txtAddedInstructionDescription.Text = string.Empty;

				MessageBox.Show("Instruction removed");
			}
			else if (lstAddInstruction.Items.Count != 0)
			{
				MessageBox.Show("Error: Please select an instruction to remove", "Warning");
			}
			else
			{
				MessageBox.Show("Error: There are no instructions to remove", "Warning");
			}
		}
		private void lstAddInstruction_SelectionChanged(object sender, SelectionChangedEventArgs e) // Views the added instruction details
		{
			ListViewItem selectedItem = (ListViewItem)lstAddInstruction.SelectedItem;

			if (selectedItem != null)
			{
				InstructionModel selectedInstruction = (InstructionModel)selectedItem.Tag;
				brdInstruction.SetValue(Grid.ColumnSpanProperty, 4);
				txtAddedInstructionDescription.Visibility = Visibility.Visible;
				txtAddedInstructionDescription.Text = selectedInstruction.Description;
			}
		}
		private void btnAddPlant_Click(object sender, RoutedEventArgs e) // Adding plant to Db
		{
			PlantModel newPlant = new();
			newPlant.Name = txtNewPlantName.Text.Trim();
			newPlant.Type = txtNewPlantType.Text.Trim();
			newPlant.Description = txtNewPlantDescription.Text.Trim();

			try
			{
				ValidateInputs();

				if (IsPlantAvailable(txtNewPlantName.Text))
				{
					MessageBox.Show("Plant is already in use. Please add a different plant.");
					return;
				}

				MessageBoxResult result = MessageBox.Show("Have you added all the instructions for this plant?", "Adding plant", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					using (GreenThumbDbContext context = new())
					{
						GreenThumbRepository<PlantModel> plantRepository = new(context);
						GreenThumbRepository<InstructionModel> instructionRepository = new(context);

						plantRepository.Add(newPlant);
						plantRepository.Complete();

						foreach (ListViewItem instruction in lstAddInstruction.Items)
						{
							InstructionModel selectedInstruction = (InstructionModel)instruction.Tag;
							selectedInstruction.PlantId = newPlant.PlantId;

							instructionRepository.Add(selectedInstruction);
							instructionRepository.Complete();
						}
					}

					MessageBox.Show("Plant successfully added! Thank you for contributing to a greener thumb!", "Plant added");
					PlantWindow plantWindow = new();
					plantWindow.Show();
					Close();
				}
			}
			catch (ArgumentException ex)
			{
				MessageBox.Show($"Error: {ex.Message}");
			}
			catch (Exception ex)
			{
				// Handle other exceptions that might occur
				MessageBox.Show($"Unexpected error: {ex.Message}");
			}
		}
		private void ValidateInputs() // Validates inputs for the fields on the screen
		{
			ValidateNotEmptyPlant(txtNewPlantName.Text, "Plant name");
			ValidateNotEmptyPlant(txtNewPlantType.Text, "Plant type");
			ValidateNotEmptyPlant(txtNewPlantDescription.Text, "Plant description");
			ValidateNotEmptyInstruction();
		}
		private void ValidateNotEmptyPlant(string input, string fieldName) // Validates if plant information is empty or not
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentException($"{fieldName} cannot be empty.");
			}
		}
		private void ValidateNotEmptyInstruction() // Validates if there is instructions for the plant
		{
			if (lstAddInstruction.Items.Count == 0)
			{
				throw new ArgumentException("You must add atleast one instruction for the plant.");
			}
		}
		private bool IsPlantAvailable(string plantCheck) // Validates if the plant is available or not
		{
			using (GreenThumbDbContext context = new())
			{
				PlantRepository plantRepository = new(context);
				var plantInDb = plantRepository.GetPlantByName(plantCheck);

				if (plantInDb.ToString() == plantCheck || plantInDb.Count() == 0)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}
	}
}
