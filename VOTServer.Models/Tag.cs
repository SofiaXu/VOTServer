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
    public class Tag : IEntity
    {
        [Key]
        public long Id { get; set; }

        public bool? IsDelete { get; set; }

        [MaxLength(24)]
        public string Name { get; set; }

        public virtual ICollection<VideoTag> Videos { get; set; }
    }
}
