using System.ComponentModel.DataAnnotations.Schema;

namespace api.leads.DTO.Request
{
    public class CCPALeadsPostRequest
    {
        [Column(TypeName = "nvarchar(25)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(25)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string Zip { get; set; }
        [Column(TypeName = "nvarchar(254)")]
        public string Email { get; set; }
    }
}
