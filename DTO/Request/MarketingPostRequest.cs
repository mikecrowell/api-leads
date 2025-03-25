using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class MarketingPostRequest
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
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid Zip")]
        public string zip { get; set; }

        [MaxLength(150)]
        public string address { get; set; }

        [Required]
        [MaxLength(20)]
        public string homePhone { get; set; }

        [MaxLength(20)]
        public string cellPhone { get; set; }

        [MaxLength(20)]
        public string workPhone { get; set; }

        [Required]
        [MaxLength(254)]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public int? campaignId { get; set; }

        [MaxLength(100)]
        public string comments { get; set; }

        public bool? isReferral { get; set; }

        [MaxLength(6)]
        public string article { get; set; }

        [MaxLength(35)]
        public string articleDescription { get; set; }

        [MaxLength(100)]
        public string source { get; set; }

        [MaxLength(2)]
        public string language { get; set; }

        [MaxLength(5)]
        public string distributorNumber { get; set; }
    }
}
