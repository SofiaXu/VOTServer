using System.ComponentModel.DataAnnotations;

namespace VOTServer.Requests
{
    public class CreateNewCommentRequest
    {
        [Required]
        public long VideoId { get; set; }

        [Required]
        [MinLength(4)]
        public string Content { get; set; }
    }
}
