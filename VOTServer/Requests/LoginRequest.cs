using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VOTServer.Requests
{
    public class LoginRequest
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
