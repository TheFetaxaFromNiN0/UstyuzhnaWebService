using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Common;

namespace Ust.Api.Models.Views
{
    public class AfishaPopup
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public IList<Attachment> Attachments { get; set; }
    }

    public class AfishaSlim
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public Attachment Attachment { get; set; }
    }
}
