using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Core.ViewModels;
using VOTServer.Models;

namespace VOTServer.Core.Services
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> repository;

        public TagService(IRepository<Tag> repository) => this.repository = repository;

        public async Task<IEnumerable<TagViewModel>> SearchTagsAsync(string name, int page, int pageSize)
        {
            return (await repository.SearchAsync(x => x.Name.StartsWith(name), pageSize, page)).Select(x => new TagViewModel
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public async Task CreateAsync(string name)
        {
            await repository.AddAsync(new Tag { Name = name });
        }

        public async Task<bool> CheckExistsAsync(string name)
        {
            return await repository.ExistsAsync(x => x.Name.StartsWith(name));
        }

        public async Task<TagViewModel> GetTagByName(string name)
        {
            var t = await repository.FirstOrDefaultAsync(x => x.Name == name);
            return new TagViewModel
            {
                Id = t.Id,
                Name = name
            };
        }
    }
}
