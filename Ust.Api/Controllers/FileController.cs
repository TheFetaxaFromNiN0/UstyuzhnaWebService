using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Ust.Api.Controllers
{
    [Route("file")]
    public class FileController: Controller
    {
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
        public ActionResult SaveFile(IFormFile file)
        {
            byte[] filesData = null;
            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                filesData = binaryReader.ReadBytes((int)file.Length);
            }
            //запись в бд

            return Ok();
        }
    }
}
