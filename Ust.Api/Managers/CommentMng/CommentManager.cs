﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Ust.Api.Common;
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
        private readonly IHubContext<CommentHub> hubContext;

        public CommentManager(IMetaDataInfoManager metaDataInfoManager, IHubContext<CommentHub> hubContext)
        {
            this.metaDataInfoManager = metaDataInfoManager;
            this.hubContext = hubContext;
        }
        public async Task<int> SaveCommentAsync(ApplicationContext db, int metaInfoId, int metaObjectId, string message, User user)
        {
            var metaInfo = await metaDataInfoManager.GetMetaDataInfoByIdAsync(db, metaInfoId);
            if (metaInfo == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
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

            //await hubContext.Clients.All.SendAsync("NewComment", new Comment
            //{
            //    Id = comment.Id,
            //    Message = comment.Message,
            //    CreatedDate = comment.CreatedDate,
            //    CreatedBy = comment.CreatedBy,
            //    UserId = comment.UserId
            //});

            return comment.Id;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByMetaInfoAsync(ApplicationContext db, int metaInfoId, int metaObjectId)
        {
            var comments = await db.CommentHistories
                .Where(c => c.MetaDataInfoId == metaInfoId && c.MetaDataObjectId == metaObjectId).ToListAsync();
            if (!comments.Any())
            {
                throw new UstApplicationException(ErrorCode.CommentsNotFound);
            }

            return comments.Select(c => new Comment
            {
                Id = c.Id,
                Message = c.Message,
                CreatedDate = c.CreatedDate,
                CreatedBy = c.CreatedBy,
                UserId = c.UserId
            }).OrderByDescending(c => c.CreatedDate);
        }
    }
}
