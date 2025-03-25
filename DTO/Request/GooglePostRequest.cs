using System;
using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class GooglePostRequest
    {
        public string lead_id { get; set; }
        public string gcl_id { get; set; }
        [Required]
        public UserColumnData[] user_column_data { get; set;}
        public string api_version { get; set; }
        [Required]
        public long? form_id { get; set; }
        public string google_key { get; set; }
        public Boolean? is_test { get; set; }
    }

    public class UserColumnData 
    {
        [Required]
        public string column_name { get; set; }
        [Required]
        public string string_value { get; set; }
        [Required]
        public string column_id { get; set; }
    }

}
