using System.ComponentModel.DataAnnotations;
using VOTServer.Models.Interface;

namespace VOTServer.Models
{
    public class Comment : IEntity
    {
        [Key]
        public long Id { get; set; }

        public bool? IsDelete { get; set; }

        [MaxLength(140)]
        public string Content { get; set; }

        public long GoodCount { get; set; }

        public long CommenterId { get; set; }

        public User Commenter { get; set; }

        public long VideoId { get; set; }

        public Video Video { get; set; }
    }
}
