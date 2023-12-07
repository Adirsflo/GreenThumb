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

			using (GreenThumbDbContext context = new())
			{
				GreenThumbRepository<PlantModel> plantRepository = new(context);
				UserModel userId = UserManager.UserSignedIn;

				if (userId != null)
				{
					var userPlants = context.PlantGardens
						.Where(pg => pg.Garden.UserId == userId.UserId)
						.Select(pg => pg.Plant)
						.Distinct()
						.ToList();

					foreach (var plant in userPlants)
					{
						ListViewItem item = new();

						item.Content = new
						{
							Name = plant.Name,
							Type = plant.Type,
						};

						item.Tag = plant;

						lstMyPlants.Items.Add(item);
					}
				}
			}
		}
		private void txtSearchInGarden_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) // Searches for plants in the garden
		{
			string searchPlantByInput = txtSearchInGarden.Text.ToLower();

			using (GreenThumbDbContext context = new())
			{
				GreenThumbRepository<PlantModel> plantRepository = new(context);

				lstMyPlants.Items.Clear();

				UserModel user = UserManager.UserSignedIn;

				if (user != null)
				{
					var filteredPlants = context.PlantGardens
						.Where(pg => pg.Garden.UserId == user.UserId && pg.Plant.Name.ToLower().Contains(searchPlantByInput))
						.Select(pg => pg.Plant)
						.Distinct()
						.ToList();

					foreach (var plant in filteredPlants)
					{
						ListViewItem item = new();
						item.Content = new
						{
							Name = plant.Name,
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
					PlantModel selectedPlant = (PlantModel)selectedItem.Tag;
					GreenThumbRepository<PlantGardens> pgRepository = new(context);
					GreenThumbRepository<InstructionModel> instructionRepository = new(context);
					var userId = UserManager.UserSignedIn.UserId;

					var gardenId = context.Gardens
						.Where(g => g.UserId == userId)
						.Select(g => g.GardenId)
						.FirstOrDefault();

					if (gardenId != 0)
					{
						var dateAtGarden = pgRepository
							.GetAll()
							.Where(pg => pg.GardenId == gardenId && pg.PlantId == selectedPlant.PlantId)
							.Select(pg => pg.DateAtGarden)
							.FirstOrDefault();

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
		private void blkInformation_Click(object sender, RoutedEventArgs e) // TODO: Update to site
		{
			MessageBox.Show("Information about how to use the application will be displayed here!", "Information");
		}
		private void btnBack_Click(object sender, RoutedEventArgs e) // Clears tempPlantsGardens list and returns to PlantWindow
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
				PlantModel selectedPlant = (PlantModel)selectedItem.Tag;
				GreenThumbRepository<PlantGardens> pgRepository = new(context);

				var userId = UserManager.UserSignedIn.UserId;

				var gardenId = context.Gardens
					.Where(g => g.UserId == userId)
					.Select(g => g.GardenId)
					.FirstOrDefault();

				var plantId = selectedPlant.PlantId;

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

						UpdateGardenUi();
					}
				}
			}
		}
	}
}
