using VOTServer.Core.ViewModels;
using VOTServer.Models;

namespace VOTServer.Responses
{
    public class LoginResponse : JsonResponse<UserViewModel>
    {
        public string Token { get; set; }
    }
}
