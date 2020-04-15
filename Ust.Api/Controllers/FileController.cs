using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Ust.Api.Common;
using Ust.Api.Managers.FileMng;

namespace Ust.Api.Controllers
{
    [AllowAnonymous]
    [Route("file")]
    public class FileController: Controller
    {
        private readonly IConfiguration configuration;
        private readonly IFileManager fileManager;

        public FileController(IConfiguration configuration, IFileManager fileManager)
        {
            this.configuration = configuration;
            this.fileManager = fileManager;
        }

        [HttpGet]
        [Route("getFile/{fileId}/{fileName}")]
        public FileResult GetFile([Required] int fileId, string fileName)
        {
            using (var db = new ApplicationContext(configuration))
            {
                var file = fileManager.GetFile(db, fileId);
                return File(file.Data, file.ContentType);
            }
        }

        [HttpPost]
        [Route("saveFile")]
        public async Task<ActionResult<int>> SaveFile([Required]int metaObjectId, [Required] int recordId, [Required]IFormFile file, string madeBy)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var fileId = await fileManager.SaveFileAsync(db, file, null, madeBy, metaObjectId, recordId);
                    return fileId;
                }
            }
            catch (UstApplicationException e)
            {
                return BadRequest(e);
            }
        }
    }
}
