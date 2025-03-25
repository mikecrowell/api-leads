using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Response
{
    public class PossibleLeadsGetResponse
    {
        [Key]
        public long PossibleId { get; set; }
        public int CampaignId { get; set; }
        public string LeadType { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public decimal Charge { get; set; }
        public int Total { get; set; }
        public int Available { get; set; }
    }
}
