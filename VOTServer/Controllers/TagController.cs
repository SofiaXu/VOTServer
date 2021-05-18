using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Core.ViewModels;
using VOTServer.Responses;

namespace VOTServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService) => this.tagService = tagService;

        [HttpPost("{name}")]
        public async Task<JsonResponse<IEnumerable<TagViewModel>>> Search(string name, int page, int pageSize)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            return new JsonResponse<IEnumerable<TagViewModel>>
            {
                StatusCode = 200,
                Message = "OK",
                Content = await tagService.SearchTagsAsync(name, page, pageSize)
            };
        }

        [HttpGet("{name}")]
        public async Task<JsonResponse<TagViewModel>> CreateTag(string name)
        {
            await tagService.CreateAsync(name);
            return new JsonResponse<TagViewModel>
            {
                StatusCode = 200,
                Message = "OK",
                Content = await tagService.GetTagByName(name)
            };
        }
    }
}
