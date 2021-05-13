using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOTServer.Models.Interface
{
    public interface IEntity
    {
        public long Id { get; }
        public bool? IsDelete { get; set; }
    }
}
