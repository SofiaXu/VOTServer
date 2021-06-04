using System.Collections.Generic;
using System.Threading.Tasks;
using VOTServer.Core.ViewModels;

namespace VOTServer.Core.Interface
{
    public interface IVideoService : IScopedService
    {
        Task CreateAsync(VideoTagViewModel video);
        Task<VideoTagViewModel> GetVideo(long id);
        Task<IEnumerable<VideoViewModel>> GetVideos(int page, int pageSize);
        Task<IEnumerable<VideoViewModel>> SearchVideos(string name, int page, int pageSize);
        Task UpdateAsync(VideoTagViewModel video);
    }
}
