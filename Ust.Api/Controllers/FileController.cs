using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Ust.Api.Common;
using Ust.Api.Common.Auth;
using Ust.Api.Managers.FileMng;

namespace Ust.Api.Controllers
{
    [AllowAnonymous]
    [Route("file")]
    public class FileController: Controller
    {
        private readonly IConfiguration configuration;
        private readonly IFileManager fileManager;
        private readonly IUserContext userContext;

        public FileController(IConfiguration configuration, IFileManager fileManager, IUserContext userContext)
        {
            this.configuration = configuration;
            this.fileManager = fileManager;
            this.userContext = userContext;
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
                    var currentUser = await userContext.GetCurrentUserAsync();

                    var fileId = await fileManager.SaveFileAsync(db, file, currentUser, madeBy, metaObjectId, recordId);
                    return fileId;
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
        [Route("saveManyFiles")]
        public async Task<ActionResult> SaveFiles([Required] int metaObjectId, [Required] int recordId,
            IFormFileCollection files, string madeBy)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    if (files.Count == 0)
                        throw new UstApplicationException(ErrorCode.EmptyFiles);
                    
                    await fileManager.SaveFilesAsync(db, files, null, madeBy, metaObjectId, recordId);

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

        [Authorize(Roles = "root,admin")]
        [HttpPost]
        [Route("saveCompanyLogo")]
        public async Task<ActionResult<int>> SaveCompanyLogo([Required] int orgId, [Required] IFormFile logo)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    var logoId = await fileManager.SaveCompanyLogoAsync(db, logo, orgId);

                    return Ok(logoId);
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
