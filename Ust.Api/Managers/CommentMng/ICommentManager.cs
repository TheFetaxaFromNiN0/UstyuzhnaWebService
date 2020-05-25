using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Ust.Api.Common.SignalR;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Response;

namespace Ust.Api.Managers.CommentMng
{
    public interface ICommentManager
    {
        Task<CommentSavedResponse> SaveCommentAsync(ApplicationContext db, int metaInfoId, int metaObjectId, string message, User user);

        Task<IEnumerable<Comment>> GetCommentsByMetaInfoAsync(ApplicationContext db, int metaInfoId, int metaObjectId, int skip, int take);

        Task AddToGroupAsync(ApplicationContext db, int metaInfoId, int metaObjectId, string connectionId);
    }
}
