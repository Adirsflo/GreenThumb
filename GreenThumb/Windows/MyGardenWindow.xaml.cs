using GreenThumb.Database;
using GreenThumb.Managers;
using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;

namespace GreenThumb.Windows
{
	public partial class MyGardenWindow : Window
	{
		public MyGardenWindow()
		{
			InitializeComponent();

			UpdateGardenUi();
		}
		private void UpdateGardenUi() // Updates the interface with the user plants in garden
		{
			lstMyPlants.Items.Clear();
			dpAddedToGarden.Text = " ";

			using (GreenThumbDbContext context = new())
			{
				PlantRepository plantRepository = new(context);
				UserModel user = UserManager.UserSignedIn!;

				if (user != null)
				{
					var userPlant = plantRepository.GetUserPlant(user);

					foreach (var plant in userPlant)
					{
						ListViewItem item = new();

						item.Content = new
						{
							Name = plant!.Name,
							Type = plant.Type,
						};
						item.Tag = plant;

						lstMyPlants.Items.Add(item);
					}
				}
			}
		}
		private void blkInformation_Click(object sender, RoutedEventArgs e) // Information for MyGardenWindow
		{
			MessageBox.Show("Welcome to your Garden!\n\n" +
										"-You can view all your plants on the left of the screen\n" +
										"-You can search for a specific plant under \"Search in Garden\"\n" +
										"-To view a certain plant, select the plant to display information on the right side\n" +
										"-If you wish to further view the instructions of a plant, click on an instruction under \"Instructions\"\n" +
										"-To remove a plant from your garden, select the plant and then click on \"Remove\"\n" +
										"-On your upper right corner, you can return to dashboard by clicking \"Back\"", "Information - Navigation");
		}
		private void btnBack_Click(object sender, RoutedEventArgs e) // Returns to PlantWindow
		{
			PlantWindow plantWindow = new();
			plantWindow.Show();
			Close();
		}
		private void btnRemoveFromGarden_Click(object sender, RoutedEventArgs e) // Removes plant from user garden
		{
			using (GreenThumbDbContext context = new())
			{
				ListBoxItem selectedItem = (ListBoxItem)lstMyPlants.SelectedItem;

				if (selectedItem != null)
				{
					GardenRepository gardenRepository = new(context);
					GardenPlantsRepository gpRepository = new(context);
					PlantModel selectedPlant = (PlantModel)selectedItem.Tag;

					var user = UserManager.UserSignedIn!;
					var garden = gardenRepository.GetByUserId(user);
					var plant = selectedPlant;

					MessageBoxResult result = MessageBox.Show("Are you sure you want to remove plant from your garden?", "Removing plant from garden", MessageBoxButton.YesNo);

					if (result == MessageBoxResult.Yes)
					{
						var gardenPlantToRemove = gpRepository.GetUserGardenPlant(garden!, plant);

						if (gardenPlantToRemove != null)
						{
							gpRepository.Remove(gardenPlantToRemove);
							gpRepository.Complete();
							UpdateGardenUi();

							MessageBox.Show("The plant was removed from your garden!", "Plant removed from garden");
						}
					}
				}
				else
				{
					MessageBox.Show($"Error: You need to select a plant to remove it");
				}
			}
		}
		private void txtSearchInGarden_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) // Searches for plants in the garden
		{
			lstMyPlants.Items.Clear();
			string searchPlantByInput = txtSearchInGarden.Text.ToLower();

			using (GreenThumbDbContext context = new())
			{
				PlantRepository plantRepository = new(context);
				UserModel user = UserManager.UserSignedIn!;

				if (user != null)
				{
					var filterPlants = plantRepository.FilterPlantsInGarden(user, searchPlantByInput);

					foreach (var plant in filterPlants)
					{
						ListViewItem item = new();
						item.Content = new
						{
							Name = plant!.Name,
							Type = plant.Type,
						};
						item.Tag = plant;

						lstMyPlants.Items.Add(item);
					}
				}
			}
		}
		private void lstMyPlants_SelectionChanged(object sender, SelectionChangedEventArgs e) // Displays information in TextBoxes about the plant
		{
			txtPlantName.Text = string.Empty;
			txtPlantDescription.Text = string.Empty;
			dpAddedToGarden.Text = string.Empty;
			lstInstructions.Items.Clear();
			txtInstructionTitle.Text = string.Empty;
			txtInstructionDescription.Text = string.Empty;

			ListBoxItem selectedItem = (ListBoxItem)lstMyPlants.SelectedItem;

			if (selectedItem != null)
			{
				using (GreenThumbDbContext context = new())
				{
					GreenThumbRepository<InstructionModel> instructionRepository = new(context);
					GardenPlantsRepository gpRepository = new(context);
					GardenRepository gardenRepository = new(context);
					PlantModel selectedPlant = (PlantModel)selectedItem.Tag;

					var user = UserManager.UserSignedIn!;
					var garden = gardenRepository.GetByUserId(user);

					if (garden!.GardenId != 0)
					{
						var dateAtGarden = gpRepository.GetByDateAtGarden(garden, selectedPlant);

						txtPlantName.Text = selectedPlant.Name;
						txtPlantDescription.Text = selectedPlant.Description;
						dpAddedToGarden.Text = dateAtGarden.ToString();

						foreach (var instruction in instructionRepository.GetAll().ToList())
						{
							if (instruction.PlantId == selectedPlant.PlantId)
							{
								ListViewItem instructionItem = new();

								instructionItem.Content = instruction.Name;
								instructionItem.Tag = instruction;

								lstInstructions.Items.Add(instructionItem);
							}
						}
					}
				}
			}
		}
		private void lstInstructions_SelectionChanged(object sender, SelectionChangedEventArgs e) // List of instructions for the current plant
		{
			ListBoxItem selectedItem = (ListBoxItem)lstInstructions.SelectedItem;

			if (selectedItem != null)
			{
				InstructionModel selectedInstruction = (InstructionModel)selectedItem.Tag;

				txtInstructionTitle.Text = selectedInstruction.Name;
				txtInstructionDescription.Text = selectedInstruction.Description;
			}
		}
	}
}
