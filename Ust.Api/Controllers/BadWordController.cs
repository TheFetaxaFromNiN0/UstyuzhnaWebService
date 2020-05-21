using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ust.Api.Common.BadWords;
using Ust.Api.Models.Request;

namespace Ust.Api.Controllers
{
    [Route("badWordInsert")]
    public class BadWordController : Controller
    {
        private readonly IConfiguration configuration;

        public BadWordController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult InsertBadWordsFromTxtFile([FromBody] BadWordRequest request)
        {
            try
            {
                using (var db = new ApplicationContext(configuration))
                {
                    BadWordsReader.Insert(db, request.Path);

                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
