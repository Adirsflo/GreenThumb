using GreenThumb.Database;
using GreenThumb.Models;
using System.Windows;

namespace GreenThumb.Managers
{
	public static class UserManager
	{
		public static UserModel? UserSignedIn { get; set; }

		public static bool SignInUser(string username, string password) // Attempt to sign in user
		{
			bool isSameInputfields = false;
			bool isNotNull = true;

			using (GreenThumbDbContext context = new())
			{
				GreenThumbRepository<UserModel> userRepository = new(context);

				foreach (var user in userRepository.GetAll())
				{
					if (user.Username.ToLower() == username.ToLower() && user.Password == password)
					{
						UserSignedIn = user;
						isSameInputfields = true;
						break;
					}
					else if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
					{
						MessageBox.Show("Please fill in the fields", "Invalid login");
						isNotNull = false;
						break;
					}
				}
				if (!isSameInputfields && isNotNull)
				{
					MessageBox.Show("The username and password did not match.", "Invalid login");
				}
			}
			return isSameInputfields;
		}
	}
}
