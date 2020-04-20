using System;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Ust.Api.Models.YouTubeModels;

namespace Ust.Api.Controllers
{
    [AllowAnonymous]
    [Route("liveStream")]
    public class LiveStreamController : Controller
    {
        [Route("videoUrl")]
        [HttpPost]       
        public YouTubeInfo GetVideoUrl()
        {
            var _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBqxDilyzlvuTO51d638V17xVQnYeYb1GQ"
            });
            var searchListRequest = _youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = "UCUuVlxcj-XCIWDBWLTwxsJQ";
            searchListRequest.Type = "video";
            searchListRequest.PublishedAfter = DateTime.Now.Subtract(TimeSpan.FromDays(14));
            var searchListResponse = searchListRequest.Execute();
            var item = searchListResponse.Items.OrderByDescending(l => l.Snippet.PublishedAt).First();
            if (item.Snippet.LiveBroadcastContent != "none" && item.Snippet.LiveBroadcastContent != "upcoming")
                return new YouTubeInfo
                {
                    IsLiveStream = true,
                    Url = "https://www.youtube.com/embed/live_stream?channel=UCUuVlxcj-XCIWDBWLTwxsJQ"
                };
            var videoId = item.Id.VideoId;
            return new YouTubeInfo
            {
                IsLiveStream = false,
                Url = $"https://www.youtube.com/embed/{videoId}"
            };

        }
    }
}
