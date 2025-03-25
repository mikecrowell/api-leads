using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Response
{
    public class CCPAGetResponse
    {
        [Required]
        public int ResultCount { get; set; }
        public List<Result> result { get; set; }
    }
    
    public class Result
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string HomePhone { get; internal set; }
        public string CellPhone { get; internal set; }
        public string WorkPhone { get; internal set; }
        public string MainAddress { get; internal set; }
        public string City { get; internal set; }
        public string State { get; internal set; }
        public string MerchandiseDetails { get; set; }
        public string LeadType { get; internal set; }
        public string CampaignDescription { get; internal set; }
        public DateTime? PurchaseDate { get; internal set; }
        public string Zip { get; internal set; }
        public string Email { get; internal set; }
    }
}
