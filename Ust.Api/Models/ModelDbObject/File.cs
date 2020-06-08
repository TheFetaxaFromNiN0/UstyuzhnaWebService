using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class File
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string MadeBy { get; set; }

        public byte[] DataBytes { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("MetaDataInfo")]
        public int MetaDataInfoId { get; set; }
        public MetaDataInfo MetaDataInfo { get; set; }

        public int MetaDataObjectId { get; set; }
    }
}
