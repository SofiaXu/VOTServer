using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOTServer.Models;

namespace VOTServer.Core.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<IEnumerable<Favorite>> GetUserFavorites(long id, int page, int pageSize);
    }
}
