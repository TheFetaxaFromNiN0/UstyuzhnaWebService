using System;
using System.Collections.Generic;
using System.Text;

namespace Ust.ModerateService.Models
{
    public class AdsInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public Attachment Attachment { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }

    public class Attachment
    {
        public string Type { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
