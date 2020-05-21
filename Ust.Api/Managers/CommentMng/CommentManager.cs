using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Ust.Api.Common;
using Ust.Api.Common.BadWords;
using Ust.Api.Common.SignalR;
using Ust.Api.Managers.MetaDataInfoMng;
using Ust.Api.Models;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Response;

namespace Ust.Api.Managers.CommentMng
{
    public class CommentManager : ICommentManager
    {
        private readonly IMetaDataInfoManager metaDataInfoManager;

        public CommentManager(IMetaDataInfoManager metaDataInfoManager)
        {
            this.metaDataInfoManager = metaDataInfoManager;
        }
        public async Task<CommentSavedResponse> SaveCommentAsync(ApplicationContext db, int metaInfoId, int metaObjectId, string message, User user, IHubContext<CommentHub> hubContext)
        {

            var metaInfo = await metaDataInfoManager.GetMetaDataInfoByIdAsync(db, metaInfoId);
            if (metaInfo == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }

            var isModerate = FilterWord.IsModerate(db, message);
            if (!isModerate)
            {
                return new CommentSavedResponse
                {
                    Id = 0,
                    IsModerate = false
                };

            }

            var comment = new CommentHistory
            {
                Message = message,
                CreatedDate = DateTimeOffset.Now,
                CreatedBy = user.UserName,
                UserId = user.Id,
                MetaDataInfoId = metaInfoId,
                MetaDataObjectId = metaObjectId
            };

            await db.CommentHistories.AddAsync(comment);
            await db.SaveChangesAsync();
            //await hubContext.Groups.AddToGroupAsync(hubContext.Clients.);
            //await hubContext.Clients.Group("cats").SendAsync("Receive", comment.Message);

            return new CommentSavedResponse
            {
                Id = comment.Id,
                IsModerate = true
            };
        }

        public async Task<IEnumerable<Comment>> GetCommentsByMetaInfoAsync(ApplicationContext db, int metaInfoId, int metaObjectId, int skip, int take)
        {
            var comments = await db.CommentHistories
                .Where(c => c.MetaDataInfoId == metaInfoId && c.MetaDataObjectId == metaObjectId).OrderBy(c => c.CreatedDate).Skip(skip).Take(take).ToListAsync();
            if (!comments.Any())
            {
                throw new UstApplicationException(ErrorCode.CommentsNotFound);
            }

            return comments.Select(c => new Comment
            {
                Id = c.Id,
                Message = c.Message,
                CreatedDate = c.CreatedDate.ToString("dd.MM.yyyy HH:mm"),
                CreatedBy = c.CreatedBy,
                UserId = c.UserId
            }).OrderByDescending(c => c.CreatedDate);
        }
    }
}
