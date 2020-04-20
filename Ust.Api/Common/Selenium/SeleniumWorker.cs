using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Ust.Api.Common.Selenium
{
    public class SeleniumWorker : ISeleniumWorker
    {
        public IList<string> GetTagsByImage(string imageUrl)
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://yandex.ru/images");
                driver.FindElementByXPath(@"/html/body/div[1]/div/div[1]/header/div/div[1]/div[2]/form/div[1]/span/span/span[2]").Click();
                driver.FindElementByXPath(@"/html/body/div[5]/div/div/div[1]/div/form[2]/span/span/input").SendKeys(imageUrl);
                driver.FindElementByClassName(@"cbir-panel__search-button").Click();
                Thread.Sleep(2500);
                var htmlTags = driver.FindElementByClassName(@"Tags_type_simple").FindElements(By.TagName("a"));

                var tags = htmlTags.Select(ht => ht.Text).ToList();
                return tags;
            }
        }
    }
}
