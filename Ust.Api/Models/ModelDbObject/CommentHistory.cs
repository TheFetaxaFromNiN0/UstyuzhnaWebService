using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api.Models
{
    public class CommentHistory
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<AlbumComment> AlbumComments { get; set; }

        public ICollection<NewsComment> NewsComments { get; set; }
    }
}
