using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Ust.Api.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedDate { get; set; }

        public ICollection<CommentHistory> Comments { get; set; }
    }

    public enum RoleType
    {
        Admin = 1,
        Regular = 2
    }
}
