using System.ComponentModel.DataAnnotations;

namespace VOTServer.Requests
{
    public class RegisterRequest
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }

        [StringLength(32)]
        [Required]
        public string Username { get; set; }

        [StringLength(11)]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [MinLength(8)]
        [Required]
        public string Password { get; set; }
    }
}
