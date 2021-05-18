using System.Collections.Generic;
using System.Threading.Tasks;
using VOTServer.Core.ViewModels;

namespace VOTServer.Core.Interface
{
    public interface ITagService : IScopedService
    {
        Task<bool> CheckExistsAsync(string name);
        Task CreateAsync(string name);
        Task<TagViewModel> GetTagByName(string name);
        Task<IEnumerable<TagViewModel>> SearchTagsAsync(string name, int page, int pageSize);
    }
}
