using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class VideoController : ControllerBase
    {
        private readonly IVideoService service;
        public VideoController(IVideoService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<JsonResponse<VideoViewModel>> GetVideo(long id)
        {
            if (id <= 0)
            {
                Response.StatusCode = 400;
                return new JsonResponse<VideoViewModel>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            var v = await service.GetVideo(id);
            if (v == null)
            {
                Response.StatusCode = 400;
                return new JsonResponse<VideoViewModel>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            return new JsonResponse<VideoViewModel>
            {
                StatusCode = 200,
                Content = v,
                Message = "OK"
            };
        }

        [HttpGet]
        public async Task<JsonResponse<IEnumerable<VideoViewModel>>> GetVideos(int page, int pageSize)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            return new JsonResponse<IEnumerable<VideoViewModel>>
            {
                StatusCode = 200,
                Message = "OK",
                Content = await service.GetVideos(page, pageSize)
            };
        }

        [HttpPost("Upload")]
        [Authorize]
        public Task<JsonResponse> UploadVideo()
        {
            throw new NotImplementedException();
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<JsonResponse> CreateVideoInformation([FromForm]CreateVideoInformationRequest request)
        {
            _ = ModelState.IsValid;
            await service.CreateAsync(new VideoTagViewModel
            {
                Info = request.Info,
                UploaderId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                Title = request.Title,
                Tags = request.Tags.Select(x => new TagViewModel { Id = x })
            });
            return new JsonResponse { StatusCode = 200, Message = "OK" };
        }

        [HttpGet("Stream/{id}")]
        public async Task<IActionResult> GetVideoFile(long id)
        {
            var v = await service.GetVideo(id);
            if (id <= 0)
            {
                return NotFound();
            }
            if (v == null)
            {
                return NotFound();
            }
            if (v.VideoStatus != VideoStatus.Normal)
            {
                return NotFound();
            }
            if (!System.IO.File.Exists($"{System.IO.Directory.GetCurrentDirectory()}/Videos/{id}.mp4"))
            {
                await service.UpdateAsync(new VideoTagViewModel { Id = id, VideoStatus = VideoStatus.Deleted });
                return NotFound();
            }
            return PhysicalFile($"{System.IO.Directory.GetCurrentDirectory()}/Videos/{id}.mp4", "video/mp4", true);
        }
    }
}
