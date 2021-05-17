using System.Collections.Generic;
using System.Threading.Tasks;
using VOTServer.Models;

namespace VOTServer.Core.Interface
{
    public interface IVideoRepository : IRepository<Video>
    {
        public Task AddTagsAsync(IEnumerable<VideoTag> tags);
    }
}
