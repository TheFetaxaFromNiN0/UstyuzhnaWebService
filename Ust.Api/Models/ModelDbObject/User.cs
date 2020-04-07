using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Ust.Api.Models.ModelDbObject
{
    public class User : IdentityUser
    {
        public DateTimeOffset CreatedDate { get; set; }

        public ICollection<CommentHistory> Comments { get; set; }

        public ICollection<File> Files { get; set; }
    }
}
