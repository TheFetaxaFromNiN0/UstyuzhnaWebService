using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Request
{
    public class CreateAlbumRequest
    {
        public string Name { get; set; }

        public int ThemeId { get; set; }
    }
}
