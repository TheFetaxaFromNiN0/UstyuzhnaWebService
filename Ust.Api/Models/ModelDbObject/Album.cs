using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ust.Api.Models.ModelDbObject
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("AlbumThemes")]
        public int ThemeId { get; set; }
        public AlbumTheme Theme { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset LastDownloadDate { get; set; }

        public int TotalPhotoCount { get; set; }

        public decimal Rating { get; set; }

        public long ViewCount { get; set; }

        public int RewiewCount { get; set; }

        public string CreatedBy { get; set; }

        public bool IsDeleted { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
