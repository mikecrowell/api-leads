
using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class CallDecisionPostRequest
    {
        [Required]
        public int? callId { get; set; }

        [Required]
        public int? decisionId { get; set; }

        [Required]
        public bool? value { get; set; }

        [MaxLength(25)]
        public string userId { get; set; }
    }
}
