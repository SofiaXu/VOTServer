using System.Collections.Generic;
using System.Threading.Tasks;
using VOTServer.Core.ViewModels;

namespace VOTServer.Core.Interface
{
    public interface ICommentService : IScopedService
    {
        Task CreateNewComment(CommentViewModel comment);
        Task<IEnumerable<CommentViewModel>> GetVideoComments(long videoId, int page, int pageSize);
    }
}
