using System.Collections.Generic;
using System.Threading.Tasks;
using VOTServer.Core.ViewModels;

namespace VOTServer.Core.Interface
{
    public interface IUserService : IScopedService
    {
        Task<UserViewModel> GetUserAsync(long id);
        Task<UserViewModel> GetUserAsync(long id, long followerId);
        Task<IEnumerable<UserViewModel>> GetUsersAsync(long followerId, int pageSize, int page);
        Task<IEnumerable<UserViewModel>> GetUsersAsync(int pageSize, int page);
        Task<IEnumerable<UserViewModel>> SearchUserAsync(string userName, int page, int pageSize);
    }
}
