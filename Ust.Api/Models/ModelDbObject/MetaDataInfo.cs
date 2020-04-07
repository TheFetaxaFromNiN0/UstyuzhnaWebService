using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class MetaDataInfo
    {
        [Key]
        public int Id { get; set; }

        public string TableName { get; set; }

        public bool HasAttachment {get; set; }

        public bool HasComment { get; set; }
    }
}
