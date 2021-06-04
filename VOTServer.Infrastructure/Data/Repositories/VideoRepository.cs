using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Models;

namespace VOTServer.Infrastructure.Data.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(VOTDbContext context) : base(context) { }

        public async Task AddTagsAsync(IEnumerable<VideoTag> tags)
        {
            await context.Set<VideoTag>().AddRangeAsync(tags);
            await context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<Video>> GetAllAsync(int pageSize, int page)
        {
            return await context.Set<Video>().Include(x => x.Uploader).Where(x => x.IsDelete != true).Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
        }

        public override async Task<Video> GetEntityByIdAsync(long id)
        {
            return await context.Set<Video>().Include(x => x.Tags).ThenInclude(t => t.Tag).Include(x => x.Uploader).FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Video>> SearchAsync(Expression<Func<Video, bool>> expression, int pageSize, int page)
        {
            return await context.Set<Video>().Include(x => x.Uploader).Where(expression).Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
        }
    }
}
