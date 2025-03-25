
namespace api.leads.DTO.Request
{
    public class FacebookPostRequest
    {
        public Entry[] entry { get; set; }
        public string _object { get; set; }
    }

    public class Entry
    {
        public Change[] changes { get; set; }
        public string id { get; set; }
        public int time { get; set; }
    }

    public class Change
    {
        public string field { get; set; }
        public Value value { get; set; }
    }

    public class Value
    {
        public string ad_id { get; set; }
        public string adgroup_id { get; set; }
        public int created_time { get; set; }
        public string form_id { get; set; }
        public string leadgen_id { get; set; }
        public string page_id { get; set; }
    }
}
