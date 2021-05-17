using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Models;

namespace VOTServer.Infrastructure.Data.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(VOTDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Comment>> GetVideoCommentsAsync(long id, int page, int pageSize)
        {
            return await context.Set<Comment>().Include(x => x.Commenter).ThenInclude(x => x.UserRole).IgnoreAutoIncludes().Where(x => x.VideoId == id).Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
        }
    }
}
