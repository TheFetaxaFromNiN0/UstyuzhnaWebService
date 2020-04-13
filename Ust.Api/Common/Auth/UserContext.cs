using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Ust.Api.Models;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api.Common.Auth
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public UserContext(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            try
            {
                var userId = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = await userManager.FindByIdAsync(userId);

                return user;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

    }
}
