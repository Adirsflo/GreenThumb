﻿using GreenThumb.Database;
using GreenThumb.Models;
using System.Windows;

namespace GreenThumb.Managers
{
    public static class UserManager
    {
        public static UserModel? UserSignedIn { get; set; } // Ta ID kanske? 

        // Funktioner som kan vara bra att ha är:
        // AddUser
        // ValidateUsername
        // SignedInUser

        public static bool SignInUser(string username, string password)
        {
            bool isSameInputfields = false;

            using (GreenThumbDbContext context = new())
            {
                GreenThumbRepository<UserModel> greenThumbRepository = new(context);

                foreach (var user in greenThumbRepository.GetAll())
                {
                    if (user.Username == username && user.Password == password)
                    {
                        UserSignedIn = user;
                        isSameInputfields = true;
                        break;
                    }
                    else if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Please fill in the fields", "Invalid login");
                    }
                }
                if (isSameInputfields == false)
                {
                    MessageBox.Show("The username and password did not match. Please try again.", "Invalid login");
                }
            }
            return isSameInputfields;
        }
    }
}