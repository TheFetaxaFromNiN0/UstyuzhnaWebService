using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ust.Api.Common.Auth;
using Ust.Api.Managers.NewsMng;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Controllers
{
    [Authorize]
    [Route("news")]
    public class NewsController : Controller
    {
        private readonly IUserContext userContext;
        private readonly IConfiguration configuration;
        private readonly INewsManager newsManager;

        public NewsController(IUserContext userContext, IConfiguration configuration, INewsManager newsManager)
        {
            this.userContext = userContext;
            this.configuration = configuration;
            this.newsManager = newsManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewsAsync([FromBody] CreateNewsRequest request)
        {
            var currentUser = await userContext.GetCurrentUserAsync();
            
            if (currentUser == null)
                return BadRequest(StatusCode(403));

            using (var db = new ApplicationContext(configuration))
            {
                await newsManager.CreateNewsAsync(db, request, currentUser);
            }

            return Ok();
        }

        [HttpGet, Route("{id}")]
        public NewsPopup GetNewsPopup(int id)
        {
            using (var db = new ApplicationContext(configuration))
            {
                return newsManager.GetNewsPopup(db, id);
            }
        }
    }
}
