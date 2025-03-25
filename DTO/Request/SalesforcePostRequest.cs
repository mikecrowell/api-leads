using System;
using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class SalesforcePostRequest
    {
        [Required]
        [MaxLength(25)]
        public string firstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string lastName { get; set; }

        [MaxLength(50)]
        public string fullName { get; set; }

        [Required]
        [MaxLength(5)]
        public string distributorNumber { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        public string distributorSalesCode { get; set; }

        [MaxLength(2)]
        public string homeClient { get; set; }

        [MaxLength(40)]
        public string homeCity { get; set; }

        [MaxLength(150)]
        public string homeStreet { get; set; }

        [MaxLength(5)]
        public string homeZip { get; set; }

        [MaxLength(2)]
        public string homeState { get; set; }

        [MaxLength(20)]
        public string homePhone { get; set; }

        [MaxLength(20)]
        public string mobilePhone { get; set; }

        [MaxLength(150)]
        public string secondaryLeadSource { get; set; }

        public DateTime purchaseDate { get; set; }

        [Required]
        [MaxLength(254)]
        public string emailAddress { get; set; }

        [MaxLength(10)]
        public string recordTypeId { get; set; }
    }
}
