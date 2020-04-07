using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
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
        [Route("getFile")]
        public FileResult GetFile(string nameFile, int parentId)
        {

            var fileContent = new byte[3];
            var contentType = "application/pdf";

            //находтить в базе по id файла массив этого файла
            return File(fileContent, contentType);
        }

        [HttpPost]
        [Route("saveFile")]
        public async Task<IActionResult> SaveFile(IFormFile file)
        {
            using (var db = new ApplicationContext(configuration))
            {
                await fileManager.SaveFileAsync(db, file, null, null);
            }

            return Ok();
        }
    }
}
