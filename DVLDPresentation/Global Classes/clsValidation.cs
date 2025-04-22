using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLDPresentation.Global_Classes
{
    public class clsValidation
    {
        public static bool ValidateEmail(string EmailAddress)
        {
            var pattern = @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.|\+)?[0-9a-zA-Z])*)@))" +
                 @"((\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-0-9a-zA-Z]*\.)+[a-zA-Z]{2,}))$";

            var regex = new Regex(pattern);

            return regex.IsMatch(EmailAddress);
        }
        public static bool ValidateInteger(string Number)
        {
            var pattern = @"^[0-9]*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }
        public static bool ValidateFloat(string Number)
        {
            var pattern = @"^[0-9]*(?:\.[0-9]*)?$";

            var regex = new Regex(pattern);

           return regex.IsMatch(Number);
        }
        public static bool IsNumber(string Number)
        {
            return (ValidateInteger(Number) || ValidateFloat(Number));
        }
    }
}
