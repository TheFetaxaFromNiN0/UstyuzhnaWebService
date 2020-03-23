using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class NewsComment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("News")]
        public int NewsId { get; set; }
        [ForeignKey("CommentHistory")]
        public int CommentId { get; set; }

        public News News { get; set; }
        public CommentHistory Comment { get; set; }
    }
}
