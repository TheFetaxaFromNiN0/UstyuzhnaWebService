using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Ust.ModerateService.Models;

namespace Ust.ModerateService.ClientApi
{
    public class ClientApi
    {
        private readonly string apiBaseUrl;

        public ClientApi()
        {
            apiBaseUrl = Config.GetString("Ust.Api.BaseUrl");
        }

        public IList<AdsInfo> GetNonModerateAds()
        {
            using (var httpClient = new HttpClient { Timeout = new TimeSpan(0, 0, 5) })
            {
                var query = apiBaseUrl + "serviceApi/getNonModerateAds";
                var response = httpClient.GetAsync(query).Result;

                HandleException(response);

                var content = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<IList<AdsInfo>>(content);

                return result;
            }
        }

        public void SetStatus(IList<AutoModerateAds> autoModerate)
        {
            using (var httpClient = new HttpClient { Timeout = new TimeSpan(0, 0, 5) })
            {
                var query = apiBaseUrl + "serviceApi/setModerateStatus";

                var request = JsonConvert.SerializeObject(autoModerate);

                var response = httpClient.PostAsync(query, new StringContent(request, Encoding.UTF8, "application/json")).Result;
                HandleException(response);
            }
        }

        private static void HandleException(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var content = response.Content.ReadAsStringAsync().Result;

            throw new Exception($"UST API responded with code {response.StatusCode}. RequestUri: {response.RequestMessage.RequestUri}. Content: {content}");
        }

    }
}
