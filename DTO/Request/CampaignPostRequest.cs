using System;
using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class CampaignPostRequest
    {
        [Required]
        public int? campaignId { get; set; }

        [Required]
        [MaxLength(100)]
        public string description { get; set; }

        [Required]
        public DateTime? startDate { get; set; }

        [Required]
        public DateTime? endDate { get; set; }

        [Required]
        public bool? leadEntry { get; set; }

        [Required]
        public int? leadTypeId { get; set; }

        [Required]
        public decimal? charge { get; set; }

        [Required]
        [MaxLength(2)]
        public string client { get; set; }

        [MaxLength(30)]
        public string navigateUrl { get; set; }

        [MaxLength(255)]
        public string scriptUrl { get; set; }

        public string facebookId { get; set; }

        [MaxLength(25)]
        public string userId { get; set; }

        [MaxLength(20)]
        public string phoneNumber { get; set; }
    }
}
