using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Core.ViewModels;
using VOTServer.Models;

namespace VOTServer.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository repository;
        public CommentService(ICommentRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateNewComment(CommentViewModel comment)
        {
            await repository.AddAsync(new Comment
            {
                CommenterId = comment.Commenter.Id,
                Content = comment.Content,
                VideoId = comment.Video.Id
            });
        }

        public async Task<IEnumerable<CommentViewModel>> GetVideoComments(long videoId, int page, int pageSize)
        {
            return (await repository.GetVideoCommentsAsync(videoId, page, pageSize))
                .Select(x => new CommentViewModel
                {
                    Commenter = new UserViewModel
                    {
                        UserName = x.Commenter.UserName,
                        Id = x.Commenter.Id,
                        UserRole = new UserRoleViewModel
                        {
                            Id = x.Commenter.UserRole.Id,
                            Name = x.Commenter.UserRole.Name
                        }
                    },
                    Content = x.Content,
                    Id = x.Id,
                    GoodCount = x.GoodCount
                });
        }
    }
}
