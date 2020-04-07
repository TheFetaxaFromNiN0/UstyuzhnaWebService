using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Request
{
    public class CreateMetaInfoRequest
    {
        [Required]
        public string TableName { get; set; }

        [Required]
        public bool HasAttachment { get; set; }

        [Required]
        public bool HasComment { get; set; }
    }
}
