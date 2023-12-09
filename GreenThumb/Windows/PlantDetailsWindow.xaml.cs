using GreenThumb.Database;
using GreenThumb.Managers;
using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;

namespace GreenThumb.Windows
{
	public partial class PlantDetailsWindow : Window
	{
		// Declaring _item and _userAddedToGarden for access in all events in window
		ListBoxItem _item;
		private bool _userAddedToGarden;

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
				lstInstructions.Items.Clear();
				using (GreenThumbDbContext context = new())
				{
					InstructionRepository instructionRepository = new(context);
					GardenRepository gardenRepository = new(context);
					GardenPlantsRepository gpRepository = new(context);
					PlantModel plantItem = (PlantModel)item.Tag;

					lblPlantName.Content = plantItem.Name;
					txtPlantDescription.Text = plantItem.Description;

					var instructions = instructionRepository.GetByPlantId(plantItem);

					foreach (var instruction in instructions)
					{
						ListViewItem instructionItem = new();

						instructionItem.Content = instruction.Name;
						instructionItem.Tag = instruction;
						lstInstructions.Items.Add(instructionItem);
					}

					UserModel user = UserManager.UserSignedIn!;

					if (user != null)
					{
						var userGarden = gardenRepository.GetByUserId(user);

						if (userGarden != null)
						{
							var gardenPlant = gpRepository.GetUserGardenPlant(userGarden, plantItem);
							bool isInGarden = gardenPlant != null;

							rbAddedToGardenTrue.IsChecked = isInGarden;
							rbAddedToGardenFalse.IsChecked = !isInGarden;
							_userAddedToGarden = isInGarden;
						}
					}
				}
			}
		}
		private void blkInformation_Click(object sender, RoutedEventArgs e) // Information for PlantDetailsWindow
		{
			MessageBox.Show("Welcome to Plant Details!\n\n" +
										"-You can view the plant information in the middle of the screen\n" +
										"-To view instruction in details, select one of \"Care Advice\"\n" +
										"-To add or remove the plant to/from your garden, click on one of the options under \"Added to Garden?\"\n" +
										"-On your upper right corner, you can return to dashboard by click on \"Back\"", "Information - Navigation");
		}
		private void btnBack_Click(object sender, RoutedEventArgs e) // Returns to PlantWindow
		{
			PlantWindow plantWindow = new PlantWindow();
			plantWindow.Show();
			Close();
		}
		private void rbAddedToGardenTrue_Checked(object sender, RoutedEventArgs e) // Adds the plant to user garden
		{
			if (_item != null && !_userAddedToGarden)
			{
				using (GreenThumbDbContext context = new())
				{
					MessageBoxResult result = MessageBox.Show("Are you sure you want to add the plant to your garden?", "Adding plant to garden", MessageBoxButton.YesNo);

					if (result == MessageBoxResult.Yes)
					{
						GardenRepository gardenRepository = new(context);
						GardenPlantsRepository gpRepository = new(context);
						UserModel user = UserManager.UserSignedIn!;

						var gardenId = gardenRepository.GetByUserId(user)!.GardenId;

						if (gardenId != 0)
						{
							PlantModel plantItem = (PlantModel)_item.Tag;

							var plantGarden = new GardenPlants()
							{
								GardenId = gardenId,
								PlantId = plantItem.PlantId,
								DateAtGarden = DateTime.Now,
							};

							gpRepository.Add(plantGarden);
							gpRepository.Complete();

							MessageBox.Show("The plant has been added to your garden!", "Plant added to garden");
							_userAddedToGarden = true;
						}
					}
					else
					{
						_userAddedToGarden = false;
						rbAddedToGardenFalse.IsChecked = true;
					}
				}
			}
		}
		private void rbAddedToGardenFalse_Checked(object sender, RoutedEventArgs e) // Removes the plant from user garden
		{
			if (_item != null && _userAddedToGarden)
			{
				using (GreenThumbDbContext context = new())
				{
					GardenRepository gardenRepository = new(context);
					GardenPlantsRepository gpRepository = new(context);
					PlantModel plantItem = (PlantModel)_item.Tag;

					var userId = UserManager.UserSignedIn!;
					var garden = gardenRepository.GetByUserId(userId)!;
					var plant = plantItem;

					MessageBoxResult result = MessageBox.Show("Are you sure you want to remove plant from your garden?", "Removing plant from garden", MessageBoxButton.YesNo);

					if (result == MessageBoxResult.Yes)
					{
						var gardenPlantToRemove = gpRepository.GetUserGardenPlant(garden, plant);

						if (gardenPlantToRemove != null)
						{
							gpRepository.Remove(gardenPlantToRemove);
							gpRepository.Complete();

							MessageBox.Show("The plant was removed from your garden!", "Plant removed from garden");
							_userAddedToGarden = false;
						}
					}
					else
					{
						_userAddedToGarden = true;
						rbAddedToGardenTrue.IsChecked = true;
					}
				}
			}
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
	}
}
