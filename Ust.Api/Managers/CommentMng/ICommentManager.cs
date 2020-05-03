using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Response;

namespace Ust.Api.Managers.CommentMng
{
    public interface ICommentManager
    {
        Task<int> SaveCommentAsync(ApplicationContext db, int metaInfoId, int metaObjectId, string message, User user);

        Task<IEnumerable<Comment>> GetCommentsByMetaInfoAsync(ApplicationContext db, int metaInfoId, int metaObjectId);
    }
}
