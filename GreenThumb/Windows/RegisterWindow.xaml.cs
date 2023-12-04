using System.Windows;

namespace GreenThumb.Windows
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
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
            string username;
            string firstName;
            string lastName;
            string email;
            string newPassword;
            string confirmPassword;

            try
            {
                username = txtNewUsername.Text;
            }
            catch (Exception)
            {
                if (txtNewUsername.Text) ;
                MessageBox.Show("Please enter a new username!", "Fill in the field");
                return;
            }
            try
            {
                firstName = txtNewFirstName.Text;
            }
            catch
            {

            }
        }

        private void CancelToSignInWindow() // Method for returning to SignInWindow
        {
            if (txtNewUsername.Text != "" || txtNewFirstName.Text != "" || txtNewLastName.Text != "" || txtNewPassword.Password != "" || txtConfirmPassword.Password != "" || txtNewEmail.Text != "")
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?\n\nYour progress will not be saved!", "Warning", MessageBoxButton.YesNo);
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
