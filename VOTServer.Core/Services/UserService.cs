using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Core.ViewModels;

namespace VOTServer.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserViewModel> GetUserAsync(long id)
        {
            var u = await userRepository.GetEntityByIdAsync(id);
            if (u == null)
            {
                return null;
            }
            return new UserViewModel
            {
                EmailAddress = u.EmailAddress,
                Id = u.Id,
                UserName = u.UserName,
                UserRole = new UserRoleViewModel
                {
                    AccessLevel = u.UserRole.AccessLevel,
                    Id = u.UserRole.Id,
                    Name = u.UserRole.Name
                },
                IsFollowed = false
            };
        }

        public async Task<UserViewModel> GetUserAsync(long id, long followerId)
        {
            var u = await userRepository.GetUserWithFollower(id, followerId);
            if (u == null)
            {
                return null;
            }
            return new UserViewModel
            {
                EmailAddress = u.EmailAddress,
                Id = u.Id,
                UserName = u.UserName,
                UserRole = new UserRoleViewModel
                {
                    AccessLevel = u.UserRole.AccessLevel,
                    Id = u.UserRole.Id,
                    Name = u.UserRole.Name
                },
                IsFollowed = u.IsFollowed
            };
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersAsync(long followerId, int pageSize, int page)
        {
            return (await userRepository.GetUsersWithFollower(followerId, pageSize, page))
                .Select(u => new UserViewModel
                {
                    EmailAddress = u.EmailAddress,
                    Id = u.Id,
                    UserName = u.UserName,
                    UserRole = new UserRoleViewModel
                    {
                        AccessLevel = u.UserRole.AccessLevel,
                        Id = u.UserRole.Id,
                        Name = u.UserRole.Name
                    },
                    IsFollowed = u.IsFollowed
                })
                .AsEnumerable();
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersAsync(int pageSize, int page)
        {
            return (await userRepository.GetAllAsync(pageSize, page))
                .Select(u => new UserViewModel
                {
                    EmailAddress = u.EmailAddress,
                    Id = u.Id,
                    UserName = u.UserName,
                    UserRole = new UserRoleViewModel
                    {
                        AccessLevel = u.UserRole.AccessLevel,
                        Id = u.UserRole.Id,
                        Name = u.UserRole.Name
                    },
                    IsFollowed = u.IsFollowed
                });
        }

        public async Task<IEnumerable<UserViewModel>> SearchUserAsync(string userName, int page, int pageSize)
        {
            return (await userRepository.SearchAsync(x => x.UserName == userName, pageSize, page)).Select(u => new UserViewModel
            {
                EmailAddress = u.EmailAddress,
                Id = u.Id,
                UserName = u.UserName,
                UserRole = new UserRoleViewModel
                {
                    AccessLevel = u.UserRole.AccessLevel,
                    Id = u.UserRole.Id,
                    Name = u.UserRole.Name
                },
                IsFollowed = false
            });
        }
    }
}
