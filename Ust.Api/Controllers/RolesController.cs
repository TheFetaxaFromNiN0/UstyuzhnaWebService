using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api.Controllers
{
    //[Authorize(Roles = "root")]
    [Route("roles")]
    public class RolesController: Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("createRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest();
            }
            
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(Json(result.Errors.Select(e => e.Description)));
        }

        [HttpDelete]
        [Route("deleteRole")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest();
            }

            var result = await _roleManager.DeleteAsync(new IdentityRole(roleName));

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(Json(result.Errors.Select(e => e.Description)));
        }

        [HttpGet]
        public IList<string> GetRoles()
        {
            return _roleManager.Roles.Select(r => r.Name).ToList();
        }
    }
}
