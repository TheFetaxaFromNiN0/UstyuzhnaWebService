using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Response
{
    public class FileResponse
    {
        public byte[] Data { get; set; }

        public string ContentType { get; set; }
    }
}
