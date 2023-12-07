using GreenThumb.Database;
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
		// Skapar en lista
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
				MessageBox.Show("Please fill in the instruction fields", "Warning");
			}
		}
		private void btnRemoveInstruction_Click(object sender, RoutedEventArgs e) // Removing instructions from the new plant
		{
			ListViewItem selectedItem = (ListViewItem)lstAddInstruction.SelectedItem;

			if (selectedItem != null)
			{
				lstAddInstruction.Items.Remove(selectedItem);
				MessageBox.Show("Instruction removed");
			}
			else if (lstAddInstruction.Items.Count != 0)
			{
				MessageBox.Show("Please select an instruction to remove", "Warning");
			}
			else
			{
				MessageBox.Show("There are no instructions to remove", "Warning");
			}
		}
		private void btnAddPlant_Click(object sender, RoutedEventArgs e) // Adding plant to database
		{
			/*
			 * 1. Kolla så inget är null
			 * OM NÅGOT ÄR DET... skicka felmeddelande
			 * 2. Samla information från Plant och lstAddInstruction
			 * 3. Lägg till PlantId i InstructionModel
			 * 3. Lägg till informationen i databasen
			 * 4. Skicka meddelande till användaren
			 */

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

							//var instructionToAdd = (InstructionModel)instruction;
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
		private void ValidateInputs()
		{
			ValidateNotEmptyPlant(txtNewPlantName.Text, "Plant name");
			ValidateNotEmptyPlant(txtNewPlantType.Text, "Plant type");
			ValidateNotEmptyPlant(txtNewPlantDescription.Text, "Plant description");
			ValidateNotEmptyInstruction(); // Fixa
		}
		private void ValidateNotEmptyPlant(string input, string fieldName)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentException($"{fieldName} cannot be empty.");
			}
		}
		private void ValidateNotEmptyInstruction()
		{
			if (lstAddInstruction.Items.Count == 0)
			{
				throw new ArgumentException("You must add atleast one instruction for the plant.");
			}
		}
		private bool IsPlantAvailable(string plantCheck)
		{
			using (GreenThumbDbContext context = new())
			{
				GreenThumbRepository<PlantModel> plantRepository = new(context);

				var gatherName = context.Plants.Where(p => p.Name == plantCheck).ToList();

				if (gatherName.ToString() == plantCheck || gatherName.Count == 0)
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
