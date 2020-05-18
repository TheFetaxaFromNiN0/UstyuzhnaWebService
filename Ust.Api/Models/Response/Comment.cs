using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Response
{
    public class Comment
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string UserId { get; set; }
    }
}
