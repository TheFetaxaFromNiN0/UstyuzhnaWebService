using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Ust.Api.Common;
using Ust.Api.Common.Auth;
using Ust.Api.Managers.MetaDataInfoMng;
using Ust.Api.Managers.NewsMng;
using Ust.Api.Models.Request;
using Ust.Api.Models.Response;
using Ust.Api.Models.Views;

namespace Ust.Api.Controllers
{
    [Route("news")]
    public class NewsController : Controller
    {
        private readonly IUserContext userContext;
        private readonly IConfiguration configuration;
        private readonly INewsManager newsManager;
        private readonly IMetaDataInfoManager metaDataInfoManager;

        public NewsController(IUserContext userContext, IConfiguration configuration, INewsManager newsManager, IMetaDataInfoManager metaDataInfoManager)
        {
            this.userContext = userContext;
            this.configuration = configuration;
            this.newsManager = newsManager;
            this.metaDataInfoManager = metaDataInfoManager;
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
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<NewsPopup>> GetNewsPopupAsync([Required]int id)
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
            catch (Exception e)
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

        [Authorize(Roles = "root,admin")]
        [HttpGet, Route("delete/{id}")]
        public async Task<IActionResult> DeleteNewsByIdAsync([Required] int id)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    await newsManager.DeleteNewsByIdAsync(db, id);
                    return Ok();
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("countByType")]
        public async Task<ActionResult<int>> GetCountByType(int newsType = 0)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var count = await newsManager.GetCountAsync(db, newsType);
                    return Ok(count);
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("attacmentsComments")]
        public async Task<ActionResult<HasAttachmentAndComments>> GetAttacmentsComments()
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    return await metaDataInfoManager.GetFlagsAsync(db, "News");
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
