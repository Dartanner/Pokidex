using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokidex.Helpers;

namespace Pokidex.ViewModels
{
    public partial class SignUpVM : ObservableObject
    {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _passwordConfirm;

        [ObservableProperty]
        private string _errorMessage;

        public SignUpVM()
        {
            Email = "test@test.com";
            Password = "Pa55w0rd!";
            PasswordConfirm = "Pa55w0rd!";
            ErrorMessage = "";
        }

        [RelayCommand]
        private async void LogIn()
        {
            await Shell.Current.GoToAsync("///LogIn");
        }

        [RelayCommand]
        private async void SignUp()
        {
            if (IsAuthenticated())
            {

            }
                //await Shell.Current.GoToAsync("///Scan");
        }

        private bool IsAuthenticated()
        {
            if (!HelperMethods.IsValidEmailAddress(Email))
            {
                ErrorMessage = "Invalid Email Address";
                return false;
            }

            if (!HelperMethods.IsValidPassword(Password))
            {
                ErrorMessage = "Invalid password. Passwords MUST:\n" +
                               " - Be between 8 and 16 characters in length\n " +
                               " - Contain at least 1 LOWERCASE character\n " +
                               " - Contain at least 1 UPPERCASE character\n " +
                               " - Contain at least 1 NUMBER\n" +
                               " - Contain at least 1 SPECIAL CHARACTER";
                return false;
            }

            if (!Password.Equals(PasswordConfirm))
            {
                ErrorMessage = "Passwords must match";

                return false;
            }

            return true;
        }

    }
}
