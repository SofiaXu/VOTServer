using VOTServer.Models;

namespace VOTServer.Responses
{
    public class LoginResponse : JsonResponse<User>
    {
        public string Token { get; set; }
    }
}
