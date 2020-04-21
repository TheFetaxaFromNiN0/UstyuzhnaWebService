using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ust.Api.Common;
using Ust.Api.Common.Auth;
using Ust.Api.Managers.NewsMng;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Controllers
{
    [Route("news")]
    public class NewsController : Controller
    {
        private readonly IUserContext userContext;
        private readonly IConfiguration configuration;
        private readonly INewsManager newsManager;

        public NewsController(IUserContext userContext, IConfiguration configuration, INewsManager newsManager)
        {
            this.userContext = userContext;
            this.configuration = configuration;
            this.newsManager = newsManager;
        }

        [Authorize(Roles = "root,admin")]
        [HttpPost]
        public async Task<ActionResult<int>> CreateNewsAsync([FromBody] CreateNewsRequest request)
        {
            try
            {
                var currentUser = await userContext.GetCurrentUserAsync();

                if (currentUser == null)
                    return BadRequest(StatusCode(403));

                using (var db = new ApplicationContext(configuration))
                {
                    return await newsManager.CreateNewsAsync(db, request, currentUser);
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<NewsPopup>> GetNewsPopupAsync(int id)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    return await newsManager.GetNewsPopupAsync(db, id);
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<IList<NewsSlim>> GetNewsAsync([Required] int skip, [Required] int take)
        {
            using (var db =new ApplicationContext(configuration))
            {
                return await newsManager.GetNewsAsync(db, skip, take);
            }
        }

        [HttpGet]
        [Route("newsbytype")]
        public async Task<IList<NewsSlim>> GetNewsByType([Required] int newsType, [Required] int skip, [Required] int take)
        {
            using (var db = new ApplicationContext(configuration))
            {
                return await newsManager.GetNewsByTypeAsync(db, newsType, skip, take);
            }
        }
    }
}
