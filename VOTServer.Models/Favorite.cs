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
