using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOTServer.Models;

namespace VOTServer.Responses
{
    public class LoginResponse : JsonResponse<User>
    {
        public string Token { get; set; }
    }
}
