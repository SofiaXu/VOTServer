using System.Collections.Generic;
using System.Threading.Tasks;
using VOTServer.Models;

namespace VOTServer.Core.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<IEnumerable<Favorite>> GetUserFavorites(long id, int page, int pageSize);
        public Task<IEnumerable<User>> GetUsersWithFollower(long followerId, int pageSize, int page);
        public Task<User> GetUserWithFollower(long id, long followerId);
        public Task<IEnumerable<Follow>> GetUserFollowers(long id, int pageSize, int page);
        public Task<IEnumerable<Follow>> GetUserFollowees(long id, int pageSize, int page);
    }
}
