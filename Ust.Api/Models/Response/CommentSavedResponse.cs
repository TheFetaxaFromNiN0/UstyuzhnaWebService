using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Response
{
    public class CommentSavedResponse
    {
        public int Id { get; set; }

        public bool IsModerate { get; set; }
    }
}
