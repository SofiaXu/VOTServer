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
        public string NewPassword { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
