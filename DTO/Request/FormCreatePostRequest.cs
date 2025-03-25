﻿using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
    public class FormCreatePostRequest
    {
        [Required]
        public int campaignId { get; set; }

        public int? groupId { get; set; }

        [MaxLength(68)]
        public string thirdPartyId { get; set; }

        [MaxLength(25)]
        public string userId { get; set; }
    }
}
