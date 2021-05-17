using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Core.ViewModels;
using VOTServer.Requests;
using VOTServer.Responses;

namespace VOTServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService) => this.commentService = commentService;

        [HttpGet("video/{id}")]
        public async Task<JsonResponse<IEnumerable<CommentViewModel>>> GetVideoComments(long id, int page, int pageSize)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            return new JsonResponse<IEnumerable<CommentViewModel>>
            {
                StatusCode = 200,
                Message = "OK",
                Content = await commentService.GetVideoComments(id, page, pageSize)
            };
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<JsonResponse> CreateNewComment(CreateNewCommentRequest request)
        {
            _ = ModelState.IsValid;
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await commentService.CreateNewComment(new CommentViewModel
            {
                Content = request.Content,
                Video = new VideoViewModel { Id = request.VideoId },
                Commenter = new UserViewModel { Id = userId }
            });
            return new JsonResponse
            {
                StatusCode = 200,
                Message = "OK"
            };
        }
    }
}
