using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Response
{
    public class PurchaseLeadsGetResponse
    {
        [Key]
        public int LeadID { get; set; }
        public string Client { get; set; }
        public string LeadType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string BestContactTime { get; set; }
        public bool PreviousCustomer { get; set; }
        public string TelevisionShow { get; set; }
        public string Age { get; set; }
        public string Comments { get; set; }
    }
}
