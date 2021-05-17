using System.Collections.Generic;
using System.Threading.Tasks;
using VOTServer.Models;

namespace VOTServer.Core.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetVideoCommentsAsync(long id, int page, int pageSize);
    }
}
