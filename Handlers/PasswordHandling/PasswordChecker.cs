using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OBiBiapp.Handlers.PasswordHandling
{
    public class PasswordChecker
    {
        public static bool ValidatePassword(string pass)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{12,}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            return hasNumber.IsMatch(pass) && hasUpperChar.IsMatch(pass) && hasMinimum8Chars.IsMatch(pass) && hasLowerChar.IsMatch(pass) && hasSymbols.IsMatch(pass);
        }


    }
}
