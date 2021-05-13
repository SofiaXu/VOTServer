using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VOTServer.Requests
{
    public class LoginRequest
    {
        public long UserId { get; set; }
        public string Password { get; set; }
    }
}
