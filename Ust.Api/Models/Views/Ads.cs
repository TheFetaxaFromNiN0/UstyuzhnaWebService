using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ust.Api.Common;

namespace Ust.Api.Models.Views
{
    public class AdsSlim
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public Attachment Attachment { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public byte Status { get; set; }
    }

    public class AdsPopup
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public int CategoryId { get; set; }

        public IList<Attachment> Attachments { get; set; }


    }
}
