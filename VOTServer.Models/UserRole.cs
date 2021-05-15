using VOTServer.Models.Interface;

namespace VOTServer.Models
{
    public class UserRole : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int AccessLevel { get; set; }

        public bool? IsDelete { get; set; }
    }
}
