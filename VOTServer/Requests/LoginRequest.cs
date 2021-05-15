using System.ComponentModel.DataAnnotations;

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
