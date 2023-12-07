using GreenThumb.Database;
using GreenThumb.Models;
using System.Globalization;
using System.Windows;

namespace GreenThumb.Windows
{
	public partial class RegisterWindow : Window
	{
		public RegisterWindow()
		{
			InitializeComponent();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			CancelToSignInWindow();
		}

		private async void btnRegister_Click(object sender, RoutedEventArgs e) // Method for registering a new user
		{
			// Declaring the variables
			UserModel newUser = new();
			newUser.Username = CapitalizeFirstLetter(txtNewUsername.Text.Trim().ToLower());
			newUser.FirstName = CapitalizeFirstLetter(txtNewFirstName.Text.Trim().ToLower());
			newUser.LastName = CapitalizeFirstLetter(txtNewLastName.Text.Trim().ToLower());
			newUser.Email = txtNewEmail.Text.Trim().ToLower();
			newUser.Password = txtNewPassword.Password.Trim();

			// Handeling exceptions: catches the error if its null, empty or doesnt contain "@"
			try
			{
				ValidateInputsAsync();

				if (await IsUsernameInUseAsync(txtNewUsername.Text, txtNewEmail.Text))
				{
					MessageBox.Show("Username or email is already in use. Please choose a different username or email.");
					return;
				}

				using (GreenThumbDbContext context = new())
				{
					GreenThumbRepository<UserModel> userRepository = new(context);
					GreenThumbRepository<GardenModel> gardenRepository = new(context);

					userRepository.Add(newUser);
					userRepository.Complete();

					GardenModel newGarden = new()
					{
						UserId = newUser.UserId
					};

					gardenRepository.Add(newGarden);
					gardenRepository.Complete();

				}

				MessageBox.Show("Account successfully created! Welcome to a greener life!", "Account created");

				SignInWindow signInWindow = new();
				signInWindow.Show();
				Close();
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
		private void ValidateInputsAsync()
		{
			ValidateNotEmptyAsync(txtNewUsername.Text, "Username");
			ValidateNotEmptyAsync(txtNewFirstName.Text, "First name");
			ValidateNotEmptyAsync(txtNewLastName.Text, "Last name");
			ValidateNotEmptyAsync(txtNewEmail.Text, "Email");
			ValidateEmailFormatAsync(txtNewEmail.Text);
			ValidatePasswordAsync(txtNewPassword.Password, txtConfirmPassword.Password);
		}
		private void ValidateNotEmptyAsync(string input, string fieldName)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentException($"{fieldName} cannot be empty.");
			}
		}
		private void ValidateEmailFormatAsync(string email)
		{
			if (!email.Contains("@"))
			{
				throw new ArgumentException("Invalid input for email. The email must contain the \"@\" symbol.");
			}
		}
		private void ValidatePasswordAsync(string newPassword, string confirmPassword)
		{
			if (string.IsNullOrEmpty(newPassword) && string.IsNullOrEmpty(confirmPassword))
			{
				throw new ArgumentException("Please enter a new password.");
			}
			else if (newPassword != confirmPassword)
			{
				throw new ArgumentException("The password doesn't match. Please try again.");
			}
		}
		private async Task<bool> IsUsernameInUseAsync(string usernameCheck, string emailCheck)
		{
			using (GreenThumbDbContext context = new())
			{
				UserRepository userRepository = new(context);

				if (await userRepository.GetByUsername(usernameCheck) == null)
				{
					return false;
				}
				else if (await userRepository.GetByEmail(emailCheck) == null)
				{
					return false;
				}

				return true;
			}
		}
		private void CancelToSignInWindow() // Method for returning to SignInWindow
		{
			if (txtNewUsername.Text != ""
					|| txtNewFirstName.Text != ""
					|| txtNewLastName.Text != ""
					|| txtNewPassword.Password != ""
					|| txtConfirmPassword.Password != ""
					|| txtNewEmail.Text != "")
			{
				MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?" +
						"\n\nYour progress will not be saved!",
						"Warning", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					SignInWindow signInWindow = new();
					signInWindow.Show();
					Close();
				}
			}
			else
			{
				SignInWindow signInWindow = new();
				signInWindow.Show();
				Close();
			}
		}
		private string CapitalizeFirstLetter(string name) // Capitalizing First and Last Name
		{
			TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
			return textInfo.ToTitleCase(name);
		}
	}
}
