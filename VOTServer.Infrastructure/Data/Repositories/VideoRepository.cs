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
            return await context.Set<Video>().Include(x => x.Uploader).Where(x => x.IsDelete == null).Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
        }

        public override async Task<Video> GetEntityByIdAsync(long id)
        {
            return await context.Set<Video>().Include(x => x.Tags).ThenInclude(t => t.Tag).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
