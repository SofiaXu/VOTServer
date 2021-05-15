using System;
using System.ComponentModel.DataAnnotations;
using VOTServer.Models.Interface;

namespace VOTServer.Models
{
    public class VideoTag : IRelation
    {
        [Key]
        public Guid Id { get; set; }

        public long TagId { get; set; }

        public long VideoId { get; set; }

        public Tag Tag { get; set; }

        public Video Video { get; set; }

        public bool? IsDelete { get; set; }
    }
}
