using System.Text.RegularExpressions;

namespace Pokidex.Helpers
{
    public static class HelperMethods
    {
        public static bool IsValidEmailAddress(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
                return false;

            try
            {
                return new System.Net.Mail.MailAddress(email).Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPassword(string input)
        {
            return
                input.Length >= 8 //min 8 characters
                && input.Length <= 16 //max 16 characters
                && Regex.IsMatch(input, "[A-Z]") //has upper
                && Regex.IsMatch(input, "[a-z]") //has lower
                && Regex.IsMatch(input, @"\d") //has digit
                && Regex.IsMatch(input, @"[!-/:-@\[-_{-~]") //has special character
                && !Regex.IsMatch(input, @"[^\dA-Za-z!-/:-@\[-_{-~]"); //doesn't include dodge characters
        }
    }
}
