using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ust.Api.BizRules.User;
using Ust.Api.Models;
using Ust.Api.Models.Request;

namespace Ust.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserBizRules userBizRules;

        public UserController(IUserBizRules userBizRules)
        {
            this.userBizRules = userBizRules;
        }
             
        [Route("createUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody]CreateUserRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            return Json(await userBizRules.CreateUserAsync(request));
        }
    }
}
