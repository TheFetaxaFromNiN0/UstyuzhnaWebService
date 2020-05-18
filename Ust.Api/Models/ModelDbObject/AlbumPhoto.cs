using System;
using System.ComponentModel.DataAnnotations;

namespace Ust.Api.Models.ModelDbObject
{
    public class AlbumPhoto
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset TakenDate { get; set; }

        public string TakenBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public decimal Rating { get; set; }

        public long ViewsCount { get; set; }
    }
}
