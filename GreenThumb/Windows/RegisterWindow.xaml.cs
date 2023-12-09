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

		private void btnCancel_Click(object sender, RoutedEventArgs e) // Returns to SignInWindow
		{
			CancelToSignInWindow();
		}
		private void CancelToSignInWindow() // Method for returning to SignInWindow
		{
			if (!string.IsNullOrWhiteSpace(txtNewUsername.Text)
					|| !string.IsNullOrWhiteSpace(txtNewFirstName.Text)
					|| !string.IsNullOrWhiteSpace(txtNewLastName.Text)
					|| !string.IsNullOrWhiteSpace(txtNewPassword.Password)
					|| !string.IsNullOrWhiteSpace(txtConfirmPassword.Password)
					|| !string.IsNullOrWhiteSpace(txtNewEmail.Text))
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
		private async void btnRegister_Click(object sender, RoutedEventArgs e) // Method for registering a new user
		{
			// Declaring the variables and capitalizes the first letters of each word
			UserModel newUser = new();
			newUser.Username = CapitalizeFirstLetter(txtNewUsername.Text.Trim().ToLower());
			newUser.FirstName = CapitalizeFirstLetter(txtNewFirstName.Text.Trim().ToLower());
			newUser.LastName = CapitalizeFirstLetter(txtNewLastName.Text.Trim().ToLower());
			newUser.Email = txtNewEmail.Text.Trim().ToLower();
			newUser.Password = txtNewPassword.Password.Trim();

			// Handeling exceptions: catches the error if its null, empty or doesnt contain "@" in email
			try
			{
				ValidateInputs();

				if (await IsUsernameInUseAsync(txtNewUsername.Text, txtNewEmail.Text))
				{
					throw new ArgumentException("Username or email is already in use. Please choose a different username or email.");
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
		private void ValidateInputs() // Validates the inputs from user
		{
			ValidateNotEmpty(txtNewUsername.Text, "Username");
			ValidateNotEmpty(txtNewFirstName.Text, "First name");
			ValidateNotEmpty(txtNewLastName.Text, "Last name");
			ValidateNotEmpty(txtNewEmail.Text, "Email");
			ValidateEmailFormat(txtNewEmail.Text);
			ValidatePassword(txtNewPassword.Password, txtConfirmPassword.Password);
		}
		private void ValidateNotEmpty(string input, string fieldName) // Sends error if empty
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentException($"{fieldName} cannot be empty.");
			}
		}
		private void ValidateEmailFormat(string email) // Sends error if there is no "@"
		{
			if (!email.Contains("@"))
			{
				throw new ArgumentException("Invalid input for email. The email must contain the \"@\" symbol.");
			}
		}
		private void ValidatePassword(string newPassword, string confirmPassword) // Sends error for password
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
		private async Task<bool> IsUsernameInUseAsync(string usernameCheck, string emailCheck) // Checks if username and email is in Db
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
		private string CapitalizeFirstLetter(string name) // Capitalizing First and Last Name
		{
			TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
			return textInfo.ToTitleCase(name);
		}
	}
}
