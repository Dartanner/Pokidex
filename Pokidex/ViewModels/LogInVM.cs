using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pokidex.Helpers;
using Pokidex.Models.Database;

namespace Pokidex.ViewModels
{
    public partial class LogInVM : ObservableObject
    {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _errorMessage;

        public LogInVM()
        {
            Email = "test@test.com";
            Password = "Pa55w0rd!";
            ErrorMessage = "";
        }

        [RelayCommand]
        private async void LogIn()
        {
            if (IsAuthenticated())
            {
                using var pokemonDB = new PokidexDatabase();
                if (pokemonDB.Users.Any(x =>
                        x.Email.ToLower() == Email.ToLower() &&
                        x.Password == Password))
                {
                    var user = pokemonDB.Users.Single(x => x.Email.ToLower() == Email.ToLower() && x.Password == Password);

                    var teamId = user.TeamModelId;

                    await Shell.Current.GoToAsync("///Home", new Dictionary<string, object>() { {"user", user }} );
                }
                else
                {
                    ErrorMessage = "Doesn't exist";
                }
            }
        }

        [RelayCommand]
        private async void SignUp()
        {
            await Shell.Current.GoToAsync("///SignUp");
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

            return true;
        }

    }
    

}
