using System.Text.RegularExpressions;

namespace FinanceManagementSystem.WebUI.Helpers
{
    public static class PasswordChecker
    {
        public static bool IsPasswordComplex(string password)
        {
            var hasUpperCase = new Regex(@"[A-Z]+");
            var hasLowerCase = new Regex(@"[a-z]+");
            var hasNumbers = new Regex(@"[0-9]+");
            var hasSpecialChar = new Regex(@"[\W]+");

            return hasUpperCase.IsMatch(password) &&
                   hasLowerCase.IsMatch(password) &&
                   hasNumbers.IsMatch(password) &&
                   hasSpecialChar.IsMatch(password);
        }
    }
}
