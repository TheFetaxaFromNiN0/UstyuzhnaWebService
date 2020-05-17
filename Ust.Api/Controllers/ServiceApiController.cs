using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ust.Api.Managers.AdsMng;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Controllers
{
    [Route("serviceApi")]
    public class ServiceApiController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IAdsManager adsManager;

        public ServiceApiController(IConfiguration configuration, IAdsManager adsManager)
        {
            this.configuration = configuration;
            this.adsManager = adsManager;
        }

        [HttpGet]
        [Route("getNonModerateAds")]
        public ActionResult<IList<AdsSlim>> GetNonModerateAds()
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var adsSlim = adsManager.GetAdsByCategoryAsync(db, 0, 0, 100, 1).GetAwaiter().GetResult();

                    return Ok(adsSlim);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("setModerateStatus")]
        public IActionResult SetStatus([FromBody] List<ModeratedAds> requestList)
        {
            try
            {
                using (this)
                {
                    using (var db = new ApplicationContext(configuration))
                    {
                         adsManager.SetStatusAsync(db, requestList).GetAwaiter().GetResult();

                        return Ok();
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
