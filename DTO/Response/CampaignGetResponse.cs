using System;

namespace api.leads.DTO.Response
{
    public class CampaignGetResponse
    {
        public int campaignId { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool leadEntry { get; set; }
        public int? leadTypeId { get; set; }
        public string leadTypeDescription { get; set; }
        public decimal charge { get; set; }
        public string client { get; set; }
        public string navigateUrl { get; set; }
        public string scriptUrl { get; set; }
        public string facebookId { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string lastUpdateBy { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public string phoneNumber { get; set; }
    }
}
