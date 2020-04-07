using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ust.Api.Models.Views
{
    public class NewsSlim
    {
        public string Title { get; set; }

        public int NewsType { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

    }

    public class NewsPopup
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public int NewsType { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }
}
