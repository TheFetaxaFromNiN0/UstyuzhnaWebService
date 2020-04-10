using System;
using System.Collections.Generic;
using Ust.Api.Common;

namespace Ust.Api.Models.Views
{
    public class NewsSlim
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int NewsType { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

    }

    public class NewsPopup
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public int NewsType { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public IList<Attachment> Attachments { get; set; }
    }
}
