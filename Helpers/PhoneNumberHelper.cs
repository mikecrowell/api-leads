using System;
using System.Text.RegularExpressions;

namespace api.leads.Helpers
{


    public static class PhoneNumberHelper
    {
        public static string RemoveNonNumeric(string value) => Regex.Replace(value, "[^0-9]", "");

        public static bool IsValidUSPhoneNumber(string phoneNumberToValidate)
        {
            try
            {
                //10 digit phone 
                Regex r = new Regex(@"^(\d{10})");

                //10 digit phone with 1 at start; total 11 digits
                Regex r1 = new Regex(@"^1?(\d{10})");
                string phoneNumberWithoutExtraCharacters = phoneNumberToValidate == null ? string.Empty : RemoveNonNumeric(phoneNumberToValidate);
                return phoneNumberWithoutExtraCharacters.Length switch
                {
                    10 => r.IsMatch(phoneNumberWithoutExtraCharacters),
                    11 => r1.IsMatch(phoneNumberWithoutExtraCharacters),
                    _ => false,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsValidPhoneNumber(string phoneNumberToValidate)
        {
            try
            {
                if (phoneNumberToValidate == null || phoneNumberToValidate.Trim().Length <= 0)
                {
                    //want to say they are invalid because nothing was entered, home cell or work phones are valid if at least one passes.
                    return false;
                }
                else
                {
                    //if it is not empty it is valid; for now.
                    string phoneNumberWithoutExtraCharacters = RemoveNonNumeric(phoneNumberToValidate);
                    return phoneNumberWithoutExtraCharacters.Length > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string FormatPhoneNumberWithCountryCode(string phone, string countryCode, bool isMobile)
        {
            switch (countryCode)
            {
                case "HC":
                case "CA":
                case "DR":
                    return "+1" + phone;
                case "MX":
                    return "+52" + phone;
                case "BA":
                    if (isMobile)
                    {
                        return "+549" + phone;
                    }
                    else
                    {
                        return "+54" + phone;
                    }
                case "BZ":
                    if (isMobile)
                    {
                        return "+55" + phone.Substring(0, 2) + "9" + phone.Substring(3, 8);
                    }
                    else
                    {
                        return "+55" + phone;
                    }

                case "CO":
                    return "+57" + phone;
                case "EC":
                    return "+593" + phone;
                case "PE":
                    if (isMobile)
                    {
                        return "+519" + phone;
                    }
                    else
                    {
                        return "+51" + phone;
                    }
                case "PH":
                    return "+63" + phone;
                default:
                    return phone;
            }
        }

    }
}