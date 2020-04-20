using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ust.Api.Common.Selenium
{
    public interface ISeleniumWorker
    {
        IList<string> GetTagsByImage(string imageUrl);
    }
}
