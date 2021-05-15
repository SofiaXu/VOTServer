using System;

namespace VOTServer.Models.Interface
{
    public interface IRelation
    {
        public Guid Id { get; }
        public bool? IsDelete { get; set; }
    }
}
