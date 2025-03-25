using System;

namespace api.leads.DTO.Response
{
    public class DuplicateLeadsGetResponse
    {
        public int leadId { get; set; }

        public string status { get; set; }

        public DateTime? loadDate { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string zip { get; set; }

        public string homePhone { get; set; }

        public string cellPhone { get; set; }

        public string workPhone { get; set; }

        public string email { get; set; }

        public string address { get; set; }

        public int campaignId { get; set; }

        public string source { get; set; }

        public int? duplicateId { get; set; }

        public int groupById { get; set; }
    }
}
