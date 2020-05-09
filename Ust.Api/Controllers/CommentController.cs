﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Ust.Api.Common;
using Ust.Api.Common.Auth;
using Ust.Api.Managers.CommentMng;
using Ust.Api.Models.Request;
using Ust.Api.Models.Response;

namespace Ust.Api.Controllers
{
    [Route("comment")]
    public class CommentController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUserContext userContext;
        private readonly ICommentManager commentManager;

        public CommentController(IConfiguration configuration, IUserContext userContext, ICommentManager commentManager)
        {
            this.configuration = configuration;
            this.userContext = userContext;
            this.commentManager = commentManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> SaveComment([Required] int metaInfoId,
            [Required] int metaObjectId, 
            [FromBody]SaveCommentRequest request)
        {
            try
            {
                var currentUser = await userContext.GetCurrentUserAsync();

                using (var db = new ApplicationContext(configuration))
                {
                    var commentId = await commentManager.SaveCommentAsync(db, metaInfoId, metaObjectId, request.Message, currentUser);
                    return commentId;
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments(
            [Required] int metaInfoId,
            [Required] int metaObjectId)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var comments = await commentManager.GetCommentsByMetaInfoAsync(db, metaInfoId, metaObjectId);
                    return Json(comments);
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
        }
    }
}