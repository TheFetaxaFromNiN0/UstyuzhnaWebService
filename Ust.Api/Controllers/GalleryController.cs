using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Ust.Api.Common;
using Ust.Api.Common.Auth;
using Ust.Api.Managers.GalleryMng;
using Ust.Api.Models.Request;

namespace Ust.Api.Controllers
{
    [Route("gallery")]
    public class GalleryController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUserContext userContext;
        private readonly IGalleryManager galleryManager;

        public GalleryController(IConfiguration configuration, IUserContext userContext, IGalleryManager galleryManager)
        {
            this.configuration = configuration;
            this.userContext = userContext;
            this.galleryManager = galleryManager;
        }

        [Authorize(Roles = "root,admin")]
        [HttpPost]
        public async Task<ActionResult<int>> CreateAlbumAsync([FromBody]CreateAlbumRequest request)
        {
            try
            {
                var currentUser = await userContext.GetCurrentUserAsync();
                if (currentUser == null)
                    return BadRequest(StatusCode(403));

                using (var db = new ApplicationContext(configuration))
                {
                    var albumId = await galleryManager.CreateAlbumAsync(db, request, currentUser);

                    return Ok(albumId);
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
