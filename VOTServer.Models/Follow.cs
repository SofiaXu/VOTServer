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
    public class Follow : IRelation
    {
        [Key]
        public Guid Id { get; set; }

        public bool? IsDelete { get; set; }

        public long FollowerId { get; set; }

        public long FolloweeId { get; set; }

        public DateTime FollowTime { get; set; }

        public User Follower { get; set; }

        public User Followee { get; set; }
    }
}
