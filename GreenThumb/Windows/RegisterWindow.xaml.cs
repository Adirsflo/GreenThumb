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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // 1. Gather information from the input-fields
            // 2. Check if the fields are correctly filled in
            // IF NOT CORRECT... send a warning to the user
            // 3. Check if username and Email is available
            // IF NOT AVAILABLE... send a warning to the user
            // 4. Save the information in the database and create new user
            // 5. Send information to user that the account was successfully created and return to SignInWindow


            string username;
            string firstName;
            string lastName;
            string email;
            string newPassword;
            string confirmPassword;


            // TRYING BUTTONS

            MessageBox.Show("Account created!", "Account created");

            SignInWindow signInWindow = new();
            signInWindow.Show();
            Close();
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
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?\n\nYour progress will not be saved!",
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
    }
}
