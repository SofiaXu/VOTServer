using System.ComponentModel.DataAnnotations;

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
