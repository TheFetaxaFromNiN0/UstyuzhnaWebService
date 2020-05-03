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
using Ust.Api.Managers.AfishaMng;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Controllers
{
    [Route("poster")]
    public class AfishaController : Controller
    {
        private readonly IUserContext userContext;
        private readonly IConfiguration configuration;
        private readonly IAfishaManager afishaManager;

        public AfishaController(IUserContext userContext, IConfiguration configuration, IAfishaManager afishaManager)
        {
            this.userContext = userContext;
            this.configuration = configuration;
            this.afishaManager = afishaManager;
        }

        [HttpGet]
        public async Task<ActionResult<AfishaSlim>> GetListAsync([Required] int skip, [Required] int take)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var result = await afishaManager.GetListAsync(db, skip, take);
                    return Ok(result);
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<AfishaPopup>> GetAfishPopupAsync([Required] int id)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var result = await afishaManager.GetAfishaPopupAsync(db, id);
                    return Ok(result);
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
        }
        
        [Authorize(Roles = "root,admin")]
        [HttpPost]
        public async Task<ActionResult<int>> CreateAfishaAsync([FromBody]CreateAfishaRequest request)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var currentUser = await userContext.GetCurrentUserAsync();

                    var result = await afishaManager.CreateAfishaAsync(db, request, currentUser);
                    return Ok(result);
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "root,admin")]
        [HttpGet, Route("delete/{id}")]
        public async Task<IActionResult> DeleteAfishaByIdAsync([Required] int id)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    await afishaManager.DeleteAfishaByIdAsync(db, id);
                    return Ok();
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
        }
    }
}
