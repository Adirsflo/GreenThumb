using GreenThumb.Managers;
using System.Windows;

namespace GreenThumb.Windows
{
	public partial class SignInWindow : Window
	{
		public SignInWindow()
		{
			InitializeComponent();
		}

		private void blkRegister_Click(object sender, RoutedEventArgs e) // Opens new window for user register
		{
			RegisterWindow registerWindow = new();
			registerWindow.Show();
			Close();
		}
		private void btnSignIn_Click(object sender, RoutedEventArgs e) // Attempting to log in user
		{
			string username = txtUsername.Text.ToLower();
			string password = txtPassword.Password;

			// Validates username and password & eventually sends error messages to user
			bool isSignedIn = UserManager.SignInUser(username, password);

			if (isSignedIn)
			{
				PlantWindow plantWindow = new();
				plantWindow.Show();
				Close();
			}
		}
	}
}
