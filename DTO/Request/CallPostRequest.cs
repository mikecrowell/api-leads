using System;
using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class CallPostRequest
    {
        [Required]
        public DateTime? startDate { get; set; }

        [Required]
        public DateTime? endDate { get; set; }

        [Required]
        public int? promotionId { get; set; }

        public int? reasonForCallId { get; set; }

        [MaxLength(50)]
        public string advertSource { get; set; }

        [MaxLength(25)]
        public string userId { get; set; }
    }
}
