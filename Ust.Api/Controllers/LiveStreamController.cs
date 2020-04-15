using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ust.Api.Controllers
{
    public class LiveStreamController : Controller
    {
        public void GetVideos()
        {
            var _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBqxDilyzlvuTO51d638V17xVQnYeYb1GQ"
            });
            //var searchListRequest = _youtubeService.LiveStreams.List("snippet");
            //searchListRequest.Id = "UCyshJWPeGoUEdXH2WetvtjA";
            var searchListRequest = _youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = "UCyshJWPeGoUEdXH2WetvtjA";
            var searchListResponse = searchListRequest.Execute();
            var a = searchListResponse.Items.OrderByDescending(l => l.Snippet.PublishedAt).First();
        }
    }
}
