using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api.Models.Request
{
    public class CreateNewsRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int NewsType { get; set; }

    }
}
