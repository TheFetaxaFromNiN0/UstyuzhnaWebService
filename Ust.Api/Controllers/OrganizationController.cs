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
using Ust.Api.Managers.OrganizationMng;
using Ust.Api.Models.Request;
using Ust.Api.Models.Response;
using Ust.Api.Models.Views;

namespace Ust.Api.Controllers
{
    [Route("organization")]
    public class OrganizationController : Controller
    {
        private readonly IUserContext userContext;
        private readonly IConfiguration configuration;
        private readonly IOrganizationManager organizationManager;
        private readonly IMetaDataInfoManager metaDataInfoManager;

        public OrganizationController(IUserContext userContext, IConfiguration configuration, IOrganizationManager organizationManager, IMetaDataInfoManager metaDataInfoManager)
        {
            this.userContext = userContext;
            this.configuration = configuration;
            this.organizationManager = organizationManager;
            this.metaDataInfoManager = metaDataInfoManager;
        }

        [Authorize(Roles = "root,admin")]
        [HttpPost]
        public async Task<ActionResult<int>> CreateOrganizationAsync([FromBody] CreateOrganizationRequest request)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var currentUser = await userContext.GetCurrentUserAsync();
                    if (currentUser == null)
                        return BadRequest(StatusCode(403));

                    var orgId = await organizationManager.CreateOrganizationAsync(db, request, currentUser);

                    return Ok(orgId);
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
        public async Task<ActionResult<IList<OrganizationSlim>>> GetOrganizationByTypeAsync([Required] int skip, [Required] int take, int orgType = 0)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var orgSlim = await organizationManager.GetOrganizationByTypeAsync(db, skip, take, orgType);

                    return Ok(orgSlim);
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
        public async Task<ActionResult<OrganizationPopUp>> GetOrganizationPopUpAsync([Required] int id)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var orgPopUp = await organizationManager.GetOrganizationPopUpAsync(db, id);

                    return Ok(orgPopUp);
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
        [Route("count")]
        public async Task<ActionResult<int>> GetCountByTypeAsync([Required] int skip, [Required] int take, int orgType = 0)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var count = await organizationManager.GetCountByTypeAsync(db, skip, take, orgType);

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

        [Authorize(Roles = "root,admin")]
        [HttpGet, Route("delete/{id}")]
        public async Task<IActionResult> DeleteOrganizationAsync([Required] int id)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    await organizationManager.DeleteOrganizationAsync(db, id);

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
        [Route("attacmentsComments")]
        public async Task<ActionResult<HasAttachmentAndComments>> GetAttacmentsComments()
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    return await metaDataInfoManager.GetFlagsAsync(db, "Organization");
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
