using GreenThumb.Database;
using GreenThumb.Managers;
using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;

namespace GreenThumb.Windows
{
	public partial class PlantWindow : Window
	{
		public PlantWindow()
		{
			InitializeComponent();

			UpdateUi();
		}
		public void UpdateUi() // Updates the windows when starting up
		{
			lstAllPlants.Items.Clear();

			using (GreenThumbDbContext context = new())
			{
				GreenThumbRepository<UserModel> userRepository = new(context);

				foreach (var user in userRepository.GetAll())
				{
					if (user.Username == UserManager.UserSignedIn.Username.ToString())
					{
						lblWelcomeUsername.Content = "Welcome " + user.Username.ToString(); // Insert Username
						lblFullName.Content = user.FirstName + " " + user.LastName.ToString();
					}
				}
			}

			// Fill in all plants in the lstMyTravels

			using (GreenThumbDbContext context = new())
			{
				GreenThumbRepository<PlantModel> plantRepository = new(context);

				foreach (var plant in plantRepository.GetAll())
				{
					ListViewItem item = new();

					item.Content = new
					{
						Name = plant.Name,
						Type = plant.Type,
					};
					item.Tag = plant;

					lstAllPlants.Items.Add(item);
				}
			}
		}

		private void txtSearchPlant_TextChanged(object sender, TextChangedEventArgs e) // Searches for plants by input
		{
			string searchPlantByInput = txtSearchPlant.Text.ToLower();

			using (GreenThumbDbContext context = new())
			{
				GreenThumbRepository<PlantModel> plantRepository = new(context);

				var allPlants = plantRepository.GetAll();

				lstAllPlants.Items.Clear();
				var filteredPlants = allPlants.Where(p => p.Name.ToLower().Contains(searchPlantByInput));
				foreach (var plant in filteredPlants)
				{
					ListViewItem item = new();
					item.Content = new
					{
						Name = plant.Name,
						Type = plant.Type,
					};
					item.Tag = plant;

					lstAllPlants.Items.Add(item);
				}
			}
		}

		private void blkMyGarden_Click(object sender, RoutedEventArgs e) // Accesing the user Garden
		{
			MyGardenWindow myGardenWindow = new();
			myGardenWindow.Show();
			Close();
		}

		private void blkInformation_Click(object sender, RoutedEventArgs e) // TODO: Fix this function
		{
			// FIX THIS FUNCTION
			MessageBox.Show("Information about how to use the application will be displayed here!", "Information");
		}

		private void btnDetails_Click(object sender, RoutedEventArgs e) // Shows details of the selected plant in a new window
		{
			ListBoxItem selectedItem = (ListBoxItem)lstAllPlants.SelectedItem;

			if (selectedItem != null)
			{
				PlantDetailsWindow detailsWindow = new(selectedItem);
				detailsWindow.Show();
				Close();
			}
			else
			{
				MessageBox.Show("Please select a plant to view.", "Warning");
			}
		}

		private void btnAddPlant_Click(object sender, RoutedEventArgs e) // Opens a new window for adding plant to database
		{
			AddPlantWindow addWindow = new();
			addWindow.Show();
			Close();
		}

		private void btnRemove_Click(object sender, RoutedEventArgs e) // Removes plant from database
		{
			ListBoxItem selectedItem = (ListBoxItem)lstAllPlants.SelectedItem;

			if (selectedItem != null)
			{
				MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this plant?", "Removing plant", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					using (GreenThumbDbContext context = new())
					{
						GreenThumbRepository<PlantModel> plantRepository = new(context);
						int selectedPlantId = ((PlantModel)selectedItem.Tag).PlantId;

						plantRepository.Delete(selectedPlantId);
						plantRepository.Complete();
					}

					UpdateUi();

					MessageBox.Show("Plant removed!", "Removing plant");
				}
			}
			else
			{
				MessageBox.Show("Please select a plant to remove.", "Warning");
			}
		}

		private void btnSignOut_Click(object sender, RoutedEventArgs e) // Signing out user
		{
			MessageBoxResult result = MessageBox.Show("Are you sure you want to sign out?", "Signing out", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				UserManager.UserSignedIn = null;

				MessageBox.Show("Thank you for thinking green!", "Signing out");
				SignInWindow signInWindow = new();
				signInWindow.Show();
				Close();
			}
		}

	}
}
