using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Response
{
    public class LeadsToMatchGetResponse
    {
        [Key]
        public long LeadId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
    }
}
