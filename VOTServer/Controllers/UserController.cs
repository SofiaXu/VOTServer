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
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<JsonResponse<UserViewModel>> GetUser(long id)
        {
            if (id < 1)
            {
                Response.StatusCode = 404;
                return new JsonResponse<UserViewModel> { StatusCode = 404, Message = "Not Found", Content = null };
            }
            var u = await userService.GetUserAsync(id);
            if (u == null)
            {
                Response.StatusCode = 404;
                return new JsonResponse<UserViewModel> { StatusCode = 404, Message = "Not Found", Content = null };
            }
            return new JsonResponse<UserViewModel> { StatusCode = 200, Message = "OK", Content = u };
        }

        [HttpGet]
        public async Task<JsonResponse<IEnumerable<UserViewModel>>> GetUsers(int page, int pageSize)
        {
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            if (page <= 0)
            {
                page = 1;
            }
            return new JsonResponse<IEnumerable<UserViewModel>> { StatusCode = 200, Message = "OK", Content = await userService.GetUsersAsync(pageSize, page) };
        }


    }
}
