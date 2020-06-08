﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ust.Api.Common;
using Ust.Api.Common.Auth;
using Ust.Api.Managers.AdsMng;
using Ust.Api.Managers.MetaDataInfoMng;
using Ust.Api.Models.Request;
using Ust.Api.Models.Response;
using Ust.Api.Models.Views;

namespace Ust.Api.Controllers
{
    [Route("ads")]
    public class AdvertisementController : Controller
    {
        private readonly IUserContext userContext;
        private readonly IConfiguration configuration;
        private readonly IAdsManager adsManager;
        private readonly IMetaDataInfoManager metaDataInfoManager;

        public AdvertisementController(IUserContext userContext, IConfiguration configuration, IAdsManager adsManager, IMetaDataInfoManager metaDataInfoManager)
        {
            this.userContext = userContext;
            this.configuration = configuration;
            this.adsManager = adsManager;
            this.metaDataInfoManager = metaDataInfoManager;
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateAdvertisementAsync([FromBody]CreateAdsRequest request)
        {
            try
            {
                var currentUser = await userContext.GetCurrentUserAsync();

                using (var db = new ApplicationContext(configuration))
                {
                    var adId = await adsManager.CreateAdsAsync(db, request, currentUser);

                    return Ok(adId);
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
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteAdsAsync([Required] int id)
        {
            try
            {
                var currentUser = await userContext.GetCurrentUserAsync();

                using (var db = new ApplicationContext(configuration))
                {
                    await adsManager.DeleteAdsAsync(db, id);

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

        [Authorize]
        [HttpPost]
        [Route("deleteMy/{id}")]
        public async Task<IActionResult> DeleteMyAdsAsync([Required] int id)
        {
            try
            {
                var currentUser = await userContext.GetCurrentUserAsync();

                using (var db = new ApplicationContext(configuration))
                {
                    await adsManager.DeleteMyAdsAsync(db, id, currentUser);

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

        //[HttpPost]
        //[Route("allAdswithFilter")]
        //public async Task<ActionResult<IList<AdsSlim>>> GetAllAdsWithFilter([FromBody])
        //{
        //    try
        //    {
        //        using (var db = new ApplicationContext(configuration))
        //        {
        //            var ads = await adsManager.GetAdsByCategoryAsync(db, categoryId, skip, take);

        //            return Ok(ads);
        //        }
        //    }
        //    catch (UstApplicationException e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

        [HttpGet]
        [Route("byCategory")]
        public async Task<ActionResult<IList<AdsSlim>>> GetAdsByCategoryAsync([Required] int skip, [Required] int take, int categoryId = 0)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var ads = await adsManager.GetAdsByCategoryAsync(db, categoryId, skip, take);

                    return Ok(ads);
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
        public async Task<ActionResult<AdsPopup>> GetAdsPopupAsync([Required] int id)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var ad = await adsManager.GetAdsPopupAsync(db, id);

                    return Ok(ad);
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

        [Authorize(Roles = "admin,root")]
        [HttpPost]
        [Route("adsByFilter")]
        public async Task<ActionResult<IList<AdsSlim>>> GetAdsByFilterAsync([FromBody]FilteredAds filter, [Required] int skip, [Required] int take)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var ad = await adsManager.GetAdsByFilterAsync(db, filter, skip, take);

                    return Ok(ad);
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

        [Authorize(Roles = "admin,root")]
        [HttpPost]
        [Route("setModerate")]
        public async Task<ActionResult> SetIsModerateAsync([Required] int id, [Required] byte statusCode)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    if (statusCode != 3 && statusCode != 4)
                        return BadRequest("Status of Ad is not valid");

                    var modereatedAd = new ModeratedAds
                    {
                        AdId = id,
                        Status = statusCode
                    };

                    await adsManager.SetStatusAsync(db, new List<ModeratedAds> { modereatedAd });

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
        [Authorize]
        [Route("my")]
        public async Task<ActionResult<IList<AdsSlim>>> GetMyAds([Required] int skip, [Required]int take)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var currentUser = await userContext.GetCurrentUserAsync();
                    if (currentUser == null)
                        throw new UstApplicationException(ErrorCode.UserNotFound);

                    var my = await adsManager.GetMy(db, skip, take, currentUser);

                    return Ok(my);
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


        [AllowAnonymous]
        [HttpGet]
        [Route("count")]
        public async Task<ActionResult<int>> GetCountAsync(int categoryId = 0)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var count = await adsManager.GetCountAsync(db, categoryId);

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

        [Authorize]
        [HttpGet]
        [Route("myAdsCount")]
        public async Task<ActionResult<int>> GetMyAdsCountAsync()
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var currentUser = await userContext.GetCurrentUserAsync();
                    if (currentUser == null)
                        throw new UstApplicationException(ErrorCode.UserNotFound);

                    var count = await adsManager.GetMyAdsCountAsync(db, currentUser);

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
                    return await metaDataInfoManager.GetFlagsAsync(db, "Ads");
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
