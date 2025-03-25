using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class PurchaseLeadsPostRequest
    {
        [Required]
        [MaxLength(5)]
        public string DistributorNumber { get; set; }

        [MaxLength(40)]
        public string City { get; set; }

        [Required]
        [MaxLength(2)]
        public string State { get; set; }

        [MaxLength(10)]
        public string Zip { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? CampaignId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? Total { get; set; }

        [Required]
        [MaxLength(2)]
        public string Client { get; set; }
    }
}
