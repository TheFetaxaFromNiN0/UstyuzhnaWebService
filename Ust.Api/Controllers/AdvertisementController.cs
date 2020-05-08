using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ust.Api.Common;
using Ust.Api.Common.Auth;
using Ust.Api.Managers.AdsMng;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Controllers
{
    [Route("ads")]
    public class AdvertisementController : Controller
    {
        private readonly IUserContext userContext;
        private readonly IConfiguration configuration;
        private readonly IAdsManager adsManager;

        public AdvertisementController(IUserContext userContext, IConfiguration configuration, IAdsManager adsManager)
        {
            this.userContext = userContext;
            this.configuration = configuration;
            this.adsManager = adsManager;
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
        }

        //[Authorize]
        //[HttpPost]
        //[Route("delete/{id}")]
        //public async Task<IActionResult> DeleteAdsAsync([Required] int id)
        //{

        //}

        [HttpGet]
        [Route("byCategory")]
        public async Task<ActionResult<IList<AdsSlim>>> GetAdsByCategoryAsync([Required] int categoryId, [Required] int skip, [Required] int take)
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
        }


        //[Authorize(Roles = "admin,root")]
        //[HttpGet]
        //[Route("nonModerateAds")]
        //public async Task<ActionResult<IList<>>> GetNonModerateAdsAsync()
        //{

        //}

        //[Authorize(Roles = "admin,root")]
        //[HttpPost]
        //[Route("setModerate/{id}")]
        //public async Task<ActionResult<>> SetIsModerateAsync([Required] int id)
        //{

        //}

        //[HttpGet]
        //[Authorize(Roles = "regular")]
        //[Route("my")]
        //public async Task<ActionResult<IList<>>> GetMyAds()
        //{

        //}

    }
}
