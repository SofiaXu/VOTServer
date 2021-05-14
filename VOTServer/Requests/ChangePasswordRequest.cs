using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VOTServer.Requests
{
    public class ChangePasswordRequest
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        [StringLength(11)]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
