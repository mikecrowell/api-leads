using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class AssignedLeadPostRequest
    {
        [Required]
        [MaxLength(25)]                                                                                    
        public string firstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string lastName { get; set; }

        [Required]
        [MaxLength(10)]
        public string zip { get; set; }

        [Phone]
        [MaxLength(20)]
        public string homePhone { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(254)]
        public string email { get; set; }

        [Required]
        public int? campaignId { get; set; }

        [MaxLength(150)]
        public string address { get; set; }

        [MaxLength(40)]
        public string city { get; set; }

        [MaxLength(2)]
        public string state { get; set; }

        [MaxLength(20)]
        public string cellPhone { get; set; }

        [MaxLength(20)]
        public string workPhone { get; set; }

        [MaxLength(100)]
        public string source { get; set; }

        [MaxLength(2)]
        public string language { get; set; }

        [Required]
        [MaxLength(5)]
        public string distributorNumber { get; set; }

        public bool? isReferral { get; set; }
    }
}
