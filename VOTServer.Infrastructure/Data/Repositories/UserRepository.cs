using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Models;

namespace VOTServer.Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(VOTDbContext context) : base(context) { }

        public override async Task<User> GetEntityByIdAsync(long id)
        {
            return await context.Set<User>().Include(s => s.UserRole).FirstOrDefaultAsync(u => u.Id == id);
        }

        public override async Task<IEnumerable<User>> GetAllAsync(int pageSize, int page)
        {
            return await context.Set<User>().Include(s => s.UserRole).Where(x => x.IsDelete != true).Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
        }

        public async Task<IEnumerable<Favorite>> GetUserFavorites(long id, int page, int pageSize)
        {
            return await context.Set<Favorite>().Include(x => x.Video).Where(x => x.IsDelete != true && x.UserId == id).Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
        }

        public async Task<IEnumerable<User>> GetUsersWithFollower(long followerId, int pageSize, int page)
        {
            return await context.Set<User>()
                .Include(x => x.Followers)
                .Include(x => x.UserRole)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new User
                {
                    EmailAddress = x.EmailAddress,
                    IsFollowed = x.Followers.Any(y => y.FollowerId == followerId),
                    UserRole = x.UserRole,
                    UserName = x.UserName
                })
                .ToArrayAsync();
        }

        public async Task<User> GetUserWithFollower(long id, long followerId)
        {
            return await context.Set<User>()
                .Include(x => x.Followers)
                .Include(x => x.UserRole)
                .Where(x => x.Id == id)
                .Select(x => new User
                {
                    EmailAddress = x.EmailAddress,
                    IsFollowed = x.Followers.Any(y => y.FollowerId == followerId),
                    UserRole = x.UserRole,
                    UserName = x.UserName
                }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Follow>> GetUserFollowers(long id, int pageSize, int page)
        {
            return await context
                .Set<Follow>()
                .Include(x => x.Follower)
                .ThenInclude(x => x.UserRole)
                .Where(x => x.FolloweeId == id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Follow>> GetUserFollowees(long id, int pageSize, int page)
        {
            return await context.Set<Follow>()
                .Include(x => x.Followee)
                .ThenInclude(x => x.UserRole)
                .Where(x => x.FollowerId == id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();
        }
    }
}
