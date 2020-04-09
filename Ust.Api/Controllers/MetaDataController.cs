using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public async Task SaveMetaAsync([FromBody] CreateMetaInfoRequest request)
        {
            using (var db = new ApplicationContext(configuration))
            {
                await metaDataInfoManager.SaveMetaDataAsync(db, request);
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task UpdateMetaAsync([FromBody] UpdateMetaInfoRequest request)
        {
            using (var db = new ApplicationContext(configuration))
            {
                await metaDataInfoManager.UpdateMetaDataAsync(db, request);
            }

        }

        [HttpPost]
        [Route("delete")]
        public async Task DeleteMetaAync(int id)
        {
            using (var db = new ApplicationContext(configuration))
            {
                await metaDataInfoManager.DeleteMetaDataAsync(db, id);
            }
        }

        [HttpGet]
        public ActionResult<List<MetaDataInfo>> GetMetaDataInfo()
        {
            using (var db = new ApplicationContext(configuration))
            {
               return metaDataInfoManager.GetMetaData(db);
            }
        }
    }
}
