using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api.Models
{
    public class CommentHistory
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("MetaDataInfo")]
        public int MetaDataInfoId { get; set; }
        public MetaDataInfo MetaDataInfo { get; set; }

        public int MetaDataObjectId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
