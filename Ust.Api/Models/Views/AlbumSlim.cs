using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Common;

namespace Ust.Api.Models.Views
{
    public class AlbumSlim
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ThemeId { get; set; }

        public string ThemeName { get; set; }

        public string LastDownloadDate { get; set; }

        public int TotalPhotoCount { get; set; }

        public decimal Rating { get; set; }

        public long ViewCount { get; set; }

        public int RewiewCount { get; set; }

        public string CreatedBy { get; set; }

        public Attachment Attachment { get; set; }
    }
}
