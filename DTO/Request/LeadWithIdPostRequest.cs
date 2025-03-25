using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class LeadWithIdPostRequest
    {
        [Required]
        public int leadId { get; set; }

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
