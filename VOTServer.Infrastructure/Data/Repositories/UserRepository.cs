using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return await context.Set<User>().Include(s => s.UserRole).Where(x => x.IsDelete == null).Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
        }

        public async Task<IEnumerable<Favorite>> GetUserFavorites(long id, int page, int pageSize)
        {
            return await context.Set<Favorite>().Include(x => x.Video).Where(x => x.IsDelete == null && x.UserId == id).Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
        }
    }
}
