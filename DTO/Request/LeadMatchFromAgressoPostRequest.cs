using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class LeadMatchFromAgressoPostRequest
    {
        [Required]
        public string firstName { get; set; }

        public string maternalLastName { get; set; }

        public string paternalLastName { get; set; }

        public string cellPhone { get; set; }

        public string homePhone { get; set; }

        public string workPhone { get; set; }

        public string email { get; set; }

        [Required]
        [MaxLength(2)]
        public string client { get; set; }

        [Required]
        [MaxLength(8)]
        public string customerNumber { get; set; }
        
        [Required]
        public int lastOrderNumber { get; set; }
       
    }
}
