using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Models;
using VOTServer.Responses;

namespace VOTServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IRepository<Video> repository;
        public VideoController(IRepository<Video> repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<JsonResponse<Video>> GetVideo(long id)
        {
            if (id <= 0)
            {
                Response.StatusCode = 400;
                return new JsonResponse<Video>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            var v = await repository.GetEntityByIdAsync(id);
            if (v == null)
            {
                Response.StatusCode = 400;
                return new JsonResponse<Video>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            return new JsonResponse<Video>
            {
                StatusCode = 200,
                Content = v,
                Message = "OK"
            };
        }

        [HttpGet]
        public async Task<JsonResponse<IEnumerable<Video>>> Get(int page, int pageSize)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            return new JsonResponse<IEnumerable<Video>>
            {
                StatusCode = 200,
                Message = "OK",
                Content = await repository.GetAllAsync(pageSize, page)
            };
        }

        [HttpPost("Upload")]
        [Authorize]
        public Task<JsonResponse<Video>> UploadVideo()
        {
            throw new NotImplementedException();
        }

        [HttpPost("Create")]
        [Authorize]
        public Task<JsonResponse<Video>> CreateVideoInformation()
        {
            throw new NotImplementedException();
        }

        [HttpGet("Stream/{id}")]
        public async Task<IActionResult> GetVideoFile(long id)
        {
            var v = await repository.GetEntityByIdAsync(id);
            if (id <= 0)
            {
                return NotFound();
            }
            if (v == null)
            {
                return NotFound();
            }
            if (v.IsDelete != null && v.IsDelete.Value)
            {
                return NotFound();
            }
            if (!System.IO.File.Exists($"{System.IO.Directory.GetCurrentDirectory()}/Videos/{id}.mp4"))
            {
                v.IsDelete = true;
                await repository.UpdateAsync(v);
                return NotFound();
            }
            return PhysicalFile($"{System.IO.Directory.GetCurrentDirectory()}/Videos/{id}.mp4", "video/mp4", true);
        }
    }
}
