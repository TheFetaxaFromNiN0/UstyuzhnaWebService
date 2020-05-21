using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Request
{
    public class SaveImageRequest
    {
        public string ImageName { get; set; }
        
        public int AlbumId { get; set; }

        public string TakenBy { get; set; }

        public DateTime TakenDate { get; set; }
    }
}
