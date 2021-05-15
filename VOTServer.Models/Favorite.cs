using System;
using System.ComponentModel.DataAnnotations;
using VOTServer.Models.Interface;

namespace VOTServer.Models
{
    public class Favorite : IRelation
    {
        [Key]
        public Guid Id { get; set; }

        public bool? IsDelete { get; set; }

        public long UserId { get; set; }

        public long VideoId { get; set; }

        public DateTime AddedTime { get; set; }

        public User User { get; set; }

        public Video Video { get; set; }
    }
}
