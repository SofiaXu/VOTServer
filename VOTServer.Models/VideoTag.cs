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
