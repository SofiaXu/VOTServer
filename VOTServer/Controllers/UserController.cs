using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Models;
using VOTServer.Responses;

namespace VOTServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<JsonResponse<User>> GetUser(long id)
        {
            if (id < 1)
            {
                Response.StatusCode = 404;
                return new JsonResponse<User> { StatusCode = 404, Message = "Not Found", Content = null };
            }
            var u = await repository.GetEntityByIdAsync(id);
            if (u == null)
            {
                Response.StatusCode = 404;
                return new JsonResponse<User> { StatusCode = 404, Message = "Not Found", Content = null };
            }
            return new JsonResponse<User> { StatusCode = 200, Message = "OK", Content = u };
        }

        [HttpGet]
        public async Task<JsonResponse<IEnumerable<User>>> GetUsers(int page, int pageSize)
        {
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            if (page <= 0)
            {
                page = 1;
            }
            return new JsonResponse<IEnumerable<User>> { StatusCode = 200, Message = "OK", Content = await repository.GetAllAsync(pageSize, page) };
        }
    }
}
