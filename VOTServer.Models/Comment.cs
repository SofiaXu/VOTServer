using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public long CommenterId { get;set; }

        public User Commenter { get; set; }

        public long VideoId { get; set; }

        public Video Video { get; set; }
    }
}
