using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class LeadPostRequest
    {
        [Required]
        [MaxLength(25)]
        public string firstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string lastName { get; set; }

        [MaxLength(2)]
        public string country { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string zip { get; set; }

        [Required]
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

        [MaxLength(255)]
        public string bestContactTime { get; set; }

        [Required]
        public bool? previousCustomer { get; set; }

        [MaxLength(25)]
        public string userId { get; set; }

        [MaxLength(6)]
        public string article { get; set; }

        [MaxLength(35)]
        public string articleDescription { get; set; }

        [MaxLength(255)]
        public string televisionShow { get; set; }

        [MaxLength(15)]
        public string incomingPhone { get; set; }

        [MaxLength(100)]
        public string comments { get; set; }

        [MaxLength(10)]
        public string age { get; set; }

        public bool? isReferral { get; set; }

        [MaxLength(100)]
        public string source { get; set; }

        [MaxLength(2)]
        public string language { get; set; }

        [MaxLength(5)]
        public string distributorNumber { get; set; }

        public bool? isAssigned { get; set; }
    }
}
