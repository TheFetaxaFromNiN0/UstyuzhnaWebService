using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ust.Api.Models.ModelDbObject
{
    public class AlbumPhoto
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime TakenDate { get; set; }

        public string TakenBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public decimal Rating { get; set; }

        public long ViewsCount { get; set; }

        public string CreatedBy { get; set; }

        [ForeignKey("Albums")]
        public int AlbumId { get; set; }
        public Album Album  { get; set; }
    }
}
