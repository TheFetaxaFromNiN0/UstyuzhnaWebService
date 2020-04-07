using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ust.Api.Models;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api.Controllers
{
    [Authorize(Roles ="root,admin" )]
    [Route("usersControl")]
    public class UsersController: Controller
    {
        // администрирование пользователей
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }
    }
}
