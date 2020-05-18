using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class Law
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string WrittenBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
