using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ust.Api.Models;
using Ust.Api.Models.Request;

namespace Ust.Api.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> SignUp([FromBody]SignUpRequest request)
        {
            // email validation!
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest();
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.Name,
                PhoneNumber = request.PhoneNumber,
                CreatedDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }
            else
            {
                return BadRequest(Json(result.Errors.Select(e => e.Description)));
            }

            return Ok();
        }

        [HttpPost]
        [Route("sigIn")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var result =
                await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);
            if (!result.Succeeded)
            {
                return BadRequest(Json("Invalid email or password"));
            }

            return Ok();
        }
    }
}
