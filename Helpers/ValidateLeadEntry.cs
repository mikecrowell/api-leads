using api.leads.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.leads.Helpers
{
    public static class ValidateLeadEntry
    {
        public static bool ValidateLead(LeadPostRequest leadPostRequest)
        {
          
            bool isValidPhone;
            if (leadPostRequest.country.ToUpper() == "US")
            {
                isValidPhone = PhoneNumberHelper.IsValidUSPhoneNumber(leadPostRequest.cellPhone) || PhoneNumberHelper.IsValidUSPhoneNumber(leadPostRequest.workPhone) || PhoneNumberHelper.IsValidUSPhoneNumber(leadPostRequest.homePhone);
            }
            else 
            {
                isValidPhone = PhoneNumberHelper.IsValidPhoneNumber(leadPostRequest.cellPhone) || PhoneNumberHelper.IsValidPhoneNumber(leadPostRequest.workPhone) || PhoneNumberHelper.IsValidPhoneNumber(leadPostRequest.homePhone);
            }
            bool isValidEmail = (leadPostRequest.email.Trim().Length <= 0) || EmailHelper.IsValidEmail(leadPostRequest.email);

            return isValidPhone && isValidEmail && (leadPostRequest.firstName.Trim().Length > 0 && leadPostRequest.lastName.Trim().Length > 0);
        }
    }
}