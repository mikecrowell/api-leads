
namespace api.leads.DTO.Response
{
    public class ChargesGetResponse
    {
        public int purchaseId { get; set; }
        public string distributorNumber { get; set; }
        public decimal totalCharge { get; set; }
        public int campaignId { get; set; }
        public string client { get; set; }
        public string entity { get; set; }
    }
}
