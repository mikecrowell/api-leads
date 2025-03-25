using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace api.leads.Helpers
{
    public static class EmailHelper
    {
        public static bool IsValidEmail(string email)
        {
            // a blank email is valid, it is optional
            if (email == null)
            {
                return true;
            }

            // a blank email is valid, it is optional
            if (email.Trim().Length <= 0)
            { 
                return true; 
            }

            if (email.Trim().EndsWith("."))
            {
                return false;
            }
           
            try
            {
               
                //source https://qawithexperts.com/article/asp-net/email-address-validation-in-c-with-and-without-regex/240
                Regex regex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
                Match match = regex.Match(email.Trim());
                return match.Success;
            }
            catch
            {
                return false;
            }
        }

    }

}
