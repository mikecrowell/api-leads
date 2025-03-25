
using System;

namespace api.leads.DTO.Response
{
    public class GroupsGetResponse
    {
        public int groupId { get; set; }
        public string name { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string lastUpdateBy { get; set; }
        public DateTime lastUpdateDate { get; set; }
    }
}
