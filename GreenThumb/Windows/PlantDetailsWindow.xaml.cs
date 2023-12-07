using GreenThumb.Database;
using GreenThumb.Managers;
using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;

namespace GreenThumb.Windows
{
	public partial class PlantDetailsWindow : Window
	{
		ListBoxItem _item;
		public PlantDetailsWindow(ListBoxItem item)
		{
			InitializeComponent();

			UpdateUi(item);
			_item = item;
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
							.Where(i => i.PlantId == plantItem.PlantId)
							.ToList();

					lstInstructions.Items.Clear();

					foreach (var instruction in instructions)
					{
						ListViewItem instructionItem = new();

						instructionItem.Content = instruction.Name;
						instructionItem.Tag = instruction;
						lstInstructions.Items.Add(instructionItem);
					}

					UserModel user = UserManager.UserSignedIn;

					if (user != null)
					{
						var userGardens = context.Gardens
							.FirstOrDefault(g => g.UserId == user.UserId);

						if (userGardens != null)
						{
							var plantGarden = context.PlantGardens
									.FirstOrDefault(pg => pg.GardenId == userGardens.GardenId && pg.PlantId == plantItem.PlantId);

							bool isInGarden = plantGarden != null;

							rbAddedToGardenTrue.IsChecked = isInGarden;
							rbAddedToGardenFalse.IsChecked = !isInGarden;
						}
					}
				}
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
			if (_item != null)
			{
				using (GreenThumbDbContext context = new())
				{
					MessageBoxResult result = MessageBox.Show("Are you sure you want to add the plant to your garden?", "Adding plant to garden", MessageBoxButton.YesNo);

					if (result == MessageBoxResult.Yes)
					{
						UserModel user = UserManager.UserSignedIn;

						var gardenId = context.Gardens
							.Where(g => g.UserId == user.UserId)
							.Select(g => g.GardenId)
							.FirstOrDefault();

						if (gardenId != 0)
						{
							PlantModel plantItem = (PlantModel)_item.Tag;

							var plantGarden = new PlantGardens()
							{
								GardenId = gardenId,
								PlantId = plantItem.PlantId,
								DateAtGarden = DateTime.Now,
							};

							context.PlantGardens.Add(plantGarden);
							context.SaveChanges();

							MessageBox.Show("The plant has been added to your garden!", "Plant added to garden");
						}
					}
				}
			}
		}

		private void rbAddedToGardenFalse_Checked(object sender, RoutedEventArgs e) // removes the plant from user garden
		{
			if (_item != null)
			{
				using (GreenThumbDbContext context = new())
				{
					PlantModel plantItem = (PlantModel)_item.Tag;
					GreenThumbRepository<PlantGardens> pgRepository = new(context);

					var userId = UserManager.UserSignedIn.UserId;

					var gardenId = context.Gardens
						.Where(g => g.UserId == userId)
						.Select(g => g.GardenId)
						.FirstOrDefault();

					var plantId = plantItem.PlantId;

					MessageBoxResult result = MessageBox.Show("Are you sure you want to remove plant from your garden?", "Removing plant from garden", MessageBoxButton.YesNo);

					if (result == MessageBoxResult.Yes)
					{
						var plantGardenToRemove = pgRepository
							.GetAll()
							.FirstOrDefault(pg => pg.GardenId == gardenId && pg.PlantId == plantId);

						if (plantGardenToRemove != null)
						{
							context.PlantGardens.Remove(plantGardenToRemove);
							pgRepository.Complete();

							MessageBox.Show("The plant was removed from your garden!", "Plant removed from garden");
						}
					}
				}
			}
		}
	}
}
