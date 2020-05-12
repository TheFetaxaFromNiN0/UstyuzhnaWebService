using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Serilog;
using Serilog.Events;
using Ust.ModerateService.Models;
using Ust.ModerateService.SeleniumJob;

namespace Ust.ModerateService
{
    public class ModerateWoker
    {
        private readonly TimeSpan waitAfterIteration;
        private readonly ClientApi.ClientApi clientApi;
        private readonly string apiBaseUrl;
        private readonly SelenuimWork selenuimWork;

        public ModerateWoker()
        {
            waitAfterIteration = Config.GetTimeSpan("ModerteWorker.WaitAterIteration", new TimeSpan(0, 5, 0));

            clientApi = new ClientApi.ClientApi();
            apiBaseUrl = Config.GetString("Ust.Api.BaseUrl");
            selenuimWork = new SelenuimWork();
            Log.Logger = new LoggerConfiguration().WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}").CreateLogger();
        }

        public void RunModerate()
        {
            try
            {
                do
                {
                    var timer = Stopwatch.StartNew();

                    IterateWork();
                    
                    Log.Information("Iteration is succsefuly complete");

                    
                    Log.Information($"Next iteration start in {waitAfterIteration - timer.Elapsed}");

                    while (timer.Elapsed < waitAfterIteration)
                        Thread.Sleep(1000);
                }
                while (true);
            }
            catch (OperationCanceledException e)
            {
                
                Log.Error("Moderate service stopped by cancelation token");
                throw;
            }
            catch (Exception e)
            {
                
                Log.Error($"Service error {e.Message}");
                throw;
            }

        }

        private void IterateWork()
        {
            Log.Information("Take no moderate ads...");
            var nonModerateList = clientApi.GetNonModerateAds();
            if (nonModerateList.Count == 0)
            {
                Log.Information("Nothing to moderate");
                return;
            }

            Log.Information($"Taken no moderate ads, count is {nonModerateList.Count}");
            var moderateList = new List<AutoModerateAds>();
            foreach (var nonModerate in nonModerateList)
            {
                try
                {
                    if (nonModerate.Attachment == null)
                    {
                        moderateList.Add(new AutoModerateAds
                        {
                            AdId = nonModerate.Id,
                            Status = 2
                        });
                        continue;
                    }

                    Log.Information($"Take tags by ads {nonModerate.Id} and image id {nonModerate.Attachment.Id}...");
                    //var tags = selenuimWork.GetTagsByImage(
                    //    apiBaseUrl + $"getFile/{nonModerate.Attachment.Id}/{nonModerate.Attachment.Name}");
                    var tags = selenuimWork.GetTagsByImage("https://im0-tub-ru.yandex.net/i?id=ebaf1b628ef937faf98d0fb0067bd043&n=24");
                    Log.Information($"Tags for ads : {string.Join(' ', tags)}");

                    var isModerate = СompareTagsAndTitle(tags, nonModerate.Title);
                    moderateList.Add(new AutoModerateAds
                    {
                        AdId = nonModerate.Id,
                        Status = (byte)(isModerate ? 3 : 2)
                    });
                }
                catch (Exception e)
                {
                    Log.Error($"Exception of service is {e}");
                }
            }

            Log.Information("Send status for moderated ads... ");
            clientApi.SetStatus(moderateList);
        }

        private bool СompareTagsAndTitle(IList<string> tags, string title)
        {
            var parsTag = new List<string>();
            foreach (var tag in tags)
            {
                var singleTags = tag.ToUpper().Split(' ');
                parsTag.AddRange(singleTags);
            }

            var titleUpper = title.ToUpper();
            return parsTag.Any(titleUpper.Contains);
        }
    }
}
