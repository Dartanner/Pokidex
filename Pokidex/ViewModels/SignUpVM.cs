using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokidex.Helpers;
using Pokidex.Models;
using Pokidex.Models.Database;

namespace Pokidex.ViewModels
{
    public partial class SignUpVM : ObservableObject
    {
        [ObservableProperty] 
        private string _firstName;

        [ObservableProperty]
        private string _lastName;

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
            FirstName = "Will";
            LastName = "Warren";
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
                using var pokemonDB = new PokidexDatabase();

                var user = new User(FirstName, LastName, Email, Password);
                var team = new Team("Team", user);

                user.Team = team;

                if (pokemonDB.Users.Any(x => x.Email == Email))
                {
                    ErrorMessage = "Duplicate user";
                    return;
                }

                pokemonDB.Users.Add(user);
                pokemonDB.Teams.Add(team);
                pokemonDB.SaveChanges();

                
                user.TeamModelId = team.Id;

                pokemonDB.Teams.Attach(user.Team);
                pokemonDB.SaveChanges();


                


                var databaseUser = pokemonDB.Users.Single(x => x.Email.ToLower() == Email.ToLower() && x.Password == Password);



                await Shell.Current.GoToAsync("///Home", new Dictionary<string, object>() { { "user", databaseUser } });
            } 
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

            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                ErrorMessage = "All fields are required";

                return false;
            }

            return true;
        }

    }
}
