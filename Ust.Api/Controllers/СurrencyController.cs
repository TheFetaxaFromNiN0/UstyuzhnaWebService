using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ust.Api.Models.Response;

namespace Ust.Api.Controllers
{
    [Route("currency")]
    public class СurrencyController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<Currency>> GetCurrency()
        {
            try
            {
                using (var httpClient = new HttpClient {Timeout = new TimeSpan(0, 0, 30)})
                {
                    var query = "https://www.cbr-xml-daily.ru/daily_json.js";

                    var response = await httpClient.GetAsync(query);
                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Responsed with status code {response.StatusCode}");

                    var content = response.Content.ReadAsStringAsync().Result;
                    var jObject = JObject.Parse(content);

                    var currentDate = jObject["Date"].ToString();

                    var dollar = JsonConvert.DeserializeObject<Valute>(jObject["Valute"]["USD"].ToString());
                    var euro = JsonConvert.DeserializeObject<Valute>(jObject["Valute"]["EUR"].ToString());

                    return Ok(new Currency
                    {
                        CurrentDate = currentDate,
                        Dollar = dollar.Value,
                        Euro = euro.Value
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
