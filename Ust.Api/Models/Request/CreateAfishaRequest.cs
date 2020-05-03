using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Request
{
    public class CreateAfishaRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
