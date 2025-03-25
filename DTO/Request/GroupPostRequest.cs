using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class GroupPostRequest
    {
        [Required]
        [MaxLength(100)]
        public string name { get; set; }

        [MaxLength(25)]
        public string userId { get; set; }
    }
}
