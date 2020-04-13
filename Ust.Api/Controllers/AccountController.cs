using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ust.Api.Common.Auth;
using Ust.Api.Models;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Response;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;

namespace Ust.Api.Controllers
{
    [AllowAnonymous]
    [Route("account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserContext userContext;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserContext userContext)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this.userContext = userContext;
        }

        [HttpGet]
        [Route("me")]
        public async Task<UserInfo> Me()
        {
            //var _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            //{
            //    ApiKey = "AIzaSyBqxDilyzlvuTO51d638V17xVQnYeYb1GQ"
            //});
            //var searchListRequest = _youtubeService.LiveStreams.List("snippet");
            //searchListRequest.Id = "UCyshJWPeGoUEdXH2WetvtjA";
            //var searchListRequest = _youtubeService.Search.List("snippet");
            //searchListRequest.ChannelId = "UCyshJWPeGoUEdXH2WetvtjA";
            //var searchListResponse = searchListRequest.Execute();
            //var a = searchListResponse.Items.OrderByDescending(l => l.Snippet.PublishedAt).First();

            var currentUser = await userContext.GetCurrentUserAsync();

            if (currentUser == null)
            {
                return new UserInfo
                {
                    UserType = UserType.Anonymous
                };
            }

            var roleName = (await _userManager.GetRolesAsync(currentUser)).FirstOrDefault();
            return new UserInfo
            {
                Id = currentUser.Id,
                UserName = currentUser.UserName,
                RoleName = roleName,
                CreatedDate = currentUser.CreatedDate,
                UserType = UserType.Registered
            };

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
                CreatedDate = DateTimeOffset.Now
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                await _userManager.AddToRoleAsync(user, "regular");
            }
            else
            {
                return BadRequest(Json(result.Errors.Select(e => e.Description)));
            }

            return Ok();
        }

        [HttpPost]
        [Route("signIn")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var result =
                await _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, false);
            if (!result.Succeeded)
            {
                return BadRequest("Invalid email or password");
            }

            return Ok();
        }

        [HttpGet]
        [Route("signOut")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
