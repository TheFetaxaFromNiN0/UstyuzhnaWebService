using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ust.Api.Common;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Response;
using Ust.Api.Models.Views;

namespace Ust.Api.Controllers
{
    //[Authorize(Roles ="root")]
    [Route("users")]
    public class UsersController: Controller
    {
        // администрирование пользователей
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        [HttpGet]
        public IEnumerable<UserView> GetUsers()
        {
            var root = HttpContext.User.IsInRole("root");

            var users = _userManager.Users.ToList();

            return users.Select(u => new UserView
            {
                Name = u.UserName,
                RoleName = _userManager.GetRolesAsync(u).GetAwaiter().GetResult().FirstOrDefault(),
                CreatedDate = u.CreatedDate,
            });
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult> DeleteUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new UstApplicationException(ErrorCode.UserNotFound);
            }

            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.DeleteAsync(user);

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateUser([FromBody] UserView request)
        {
            var user = await _userManager.FindByNameAsync(request.Name);
            if (user == null)
            {
                throw new UstApplicationException(ErrorCode.UserNotFound);
            }

            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            user.UserName = request.Name;
            await _userManager.UpdateAsync(user);

            if (request.RoleName != userRole)
            {
                if (userRole != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, userRole);
                }

                await _userManager.AddToRoleAsync(user, request.RoleName);
            }

            return Ok();
        }
    }
}
