using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Ust.Api.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedDate { get; set; }
    }

    public enum RoleType
    {
        Admin = 1,
        Regular = 2
    }
}
