using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ust.Api.Common;
using Ust.Api.Common.Auth;
using Ust.Api.Managers.GalleryMng;
using Ust.Api.Managers.MetaDataInfoMng;
using Ust.Api.Models.Request;
using Ust.Api.Models.Response;
using Ust.Api.Models.Views;

namespace Ust.Api.Controllers
{
    [Route("gallery")]
    public class GalleryController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUserContext userContext;
        private readonly IGalleryManager galleryManager;
        private readonly IMetaDataInfoManager metaDataInfoManager;

        public GalleryController(IConfiguration configuration, IUserContext userContext, IGalleryManager galleryManager, IMetaDataInfoManager metaDataInfoManager)
        {
            this.configuration = configuration;
            this.userContext = userContext;
            this.galleryManager = galleryManager;
            this.metaDataInfoManager = metaDataInfoManager;
        }

        [Authorize(Roles = "root,admin")]
        [HttpPost]
        [Route("album")]
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

        [HttpGet]
        [Route("album")]
        public async Task<ActionResult<IList<AlbumSlim>>> GetAlbumsAsync([Required]int skip, [Required] int take)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var albumSlims = await galleryManager.GetAlbumsAsync(db, skip, take);

                    return Ok(albumSlims);
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

        [Authorize(Roles = "root,admin")]
        [HttpPost]
        [Route("image")]
        public async Task<ActionResult<int>> SaveImageToAlbumAsync([FromBody]SaveImageRequest request)
        {
            try
            {
                var currentUser = await userContext.GetCurrentUserAsync();
                if (currentUser == null)
                    return BadRequest(StatusCode(403));

                using (var db = new ApplicationContext(configuration))
                {
                    var photoId = await galleryManager.SaveImageToAlbumAsync(db, request, currentUser);
                    return Ok(photoId);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("image/attacmentsComments")]
        public async Task<ActionResult<HasAttachmentAndComments>> GetAttacmentsComments()
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    return await metaDataInfoManager.GetFlagsAsync(db, "AlbumPhoto");
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

        [Authorize(Roles = "root,admin")]
        [HttpPost]
        [Route("theme")]
        public async Task<ActionResult<int>> CreateAlbumThemeAsync([FromBody] CreateThemeRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    return BadRequest(StatusCode(404));
                }

                var currentUser = await userContext.GetCurrentUserAsync();
                if (currentUser == null)
                    return BadRequest(StatusCode(403));

                using (var db = new ApplicationContext(configuration))
                {
                    var newThemeId = await galleryManager.CreateAlbumThemeAsync(db, request.Name);

                    return Ok(newThemeId);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
