using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class AlbumComment
    {
        public class NewsFile
        {
            [Key]
            public int Id { get; set; }

            [ForeignKey("Album")]
            public int AlbumId { get; set; }
            [ForeignKey("CommentHistory")]
            public int CommentId { get; set; }

            public Album Album { get; set; }
            public CommentHistory Comment { get; set; }

        }
    }
}
