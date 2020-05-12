using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Request
{
    public class AutoModerateAds
    {
        public int AdId { get; set; }

        public byte Status { get; set; }
    }
}
