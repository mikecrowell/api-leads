
using System;

namespace api.leads.DTO.Response
{
    public class FormsGetResponse
    {
        public int formId { get; set; }
        public int campaignId { get; set; }
        public string description { get; set; }
        public int? groupId { get; set; }
        public string groupName { get; set; }
        public string thirdPartyId { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string lastUpdateBy { get; set; }
        public DateTime lastUpdateDate { get; set; }
    }
}
