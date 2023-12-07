using GreenThumb.Database;
using GreenThumb.Managers;
using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;

namespace GreenThumb.Windows
{
	public partial class PlantDetailsWindow : Window
	{
		public PlantDetailsWindow(ListBoxItem item)
		{
			InitializeComponent();

			UpdateUi(item);
		}

		private void UpdateUi(ListBoxItem item) // Updates the user interface with plant information
		{
			if (item != null)
			{
				using (GreenThumbDbContext context = new())
				{
					PlantModel plantItem = (PlantModel)item.Tag;

					lblPlantName.Content = plantItem.Name;
					txtPlantDescription.Text = plantItem.Description;

					var instructions = context.Instructions
							.Where(i => i.PlantId == plantItem.PlantId).ToList();

					foreach (var instruction in instructions)
					{
						ListViewItem instructionItem = new();

						instructionItem.Content = instruction.Name;
						instructionItem.Tag = instruction;
						lstInstructions.Items.Add(instructionItem);
					}


					UserModel userId = UserManager.UserSignedIn;

					// TODO: Ifall man inte har plantor i sin garden så ska det automatisk stå ikryssat "No" i rbAddedToGarden

					if (userId != null)
					{
						var userGardens = context.Gardens.FirstOrDefault(g => g.UserId == userId.UserId);
						if (userGardens != null)
						{
							var plantGardenRelation = context.PlantGardens
									.Where(pg => pg.GardenId == userGardens.GardenId).ToList();

							foreach (var plantGarden in plantGardenRelation)
							{
								int currentPlantId = plantItem.PlantId;

								rbAddedToGardenTrue.IsChecked = plantGarden.PlantId == currentPlantId;
							}

							rbAddedToGardenFalse.IsChecked = !rbAddedToGardenTrue.IsChecked;
						}
					}






					/*
					foreach (var instruction in context.Instructions.Where(i => i.PlantId == plantItem.PlantId)) // TODO: Forma om koden
					{
							ListViewItem instructionItem = new();

							instructionItem.Content = instruction.Name;
							instructionItem.Tag = instruction;
							lstInstructions.Items.Add(instructionItem);
					}

					int gardenId = 0;

					foreach (var garden in context.Gardens.Where(g => g.UserId == UserManager.UserSignedIn.UserId))
					{
							gardenId = garden.GardenId;
					}

					foreach(var plantGarden in context.PlantGardens.Where(pg => pg.GardenId == gardenId))
					{

					}*/



				}
				// CLEAR lstAdInstructions
				// rbIsAddedToGarden (rbAddedToGardenTrue // rbAddedToGardenFalse)
				// Här vill du att den läser av ifall användarens GardenId har en PlantId
				// Om den har det ska "Yes" vara ifylt, osv

				/*
				 * Du vill foreacha varje garden
				 * i foreachen vill du se ifall UserId stämmer överens med det nuvarande inloggade UserId
				 * Spara GardenId
				 * 
				 * Du vill sen se ifall PlantGardens GardenId har PlantId:et som details visar
				 * Ifall det finns en PlantId i GardenId, "Yes" ska vara ifyllt
				 * Annars "No"
				 */

			}
		}

		private void blkInformation_Click(object sender, RoutedEventArgs e) // TODO: Display information about the window
		{
			// TODO: Display information about the current window
			MessageBox.Show("Information about this window till be showed here", "Information");
		}

		private void btnBack_Click(object sender, RoutedEventArgs e) // Returns to PlantWindow
		{
			PlantWindow plantWindow = new PlantWindow();
			plantWindow.Show();
			Close();
		}

		private void lstInstruction_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) // List of instructions for the current plant
		{
			ListBoxItem selectedItem = (ListBoxItem)lstInstructions.SelectedItem;

			if (selectedItem != null)
			{
				InstructionModel selectedInstruction = (InstructionModel)selectedItem.Tag;

				txtInstructionName.Text = selectedInstruction.Name;
				txtInstructionDescription.Text = selectedInstruction.Description;
			}
		}

		private void rbAddedToGardenTrue_Checked(object sender, RoutedEventArgs e) // Adds the plant to user garden
		{

		}

		private void rbAddedToGardenFalse_Checked(object sender, RoutedEventArgs e) // removes the plant from user garden
		{

		}
	}
}
