using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ust.Api.Common;
using Ust.Api.Managers.MetaDataInfoMng;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;

namespace Ust.Api.Controllers
{
    [Authorize]
    [Route("metaData")]
    public class MetaDataController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IMetaDataInfoManager metaDataInfoManager;

        public MetaDataController(IConfiguration configuration, IMetaDataInfoManager metaDataInfoManager)
        {
            this.configuration = configuration;
            this.metaDataInfoManager = metaDataInfoManager;
        }

        [HttpPost]
        [Route("save")]
        public async Task<ActionResult> SaveMetaAsync([FromBody] CreateMetaInfoRequest request)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    await metaDataInfoManager.SaveMetaDataAsync(db, request);
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

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateMetaAsync([FromBody] UpdateMetaInfoRequest request)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    await metaDataInfoManager.UpdateMetaDataAsync(db, request);
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

        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult> DeleteMetaAync(int id)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    await metaDataInfoManager.DeleteMetaDataAsync(db, id);

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
        public async Task<ActionResult<List<MetaDataInfo>>> GetMetaDataInfo()
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var result = await metaDataInfoManager.GetMetaDataAsync(db);
                    return Ok(result);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
