using System;
using System.Text.Json.Serialization;

namespace Ust.Api.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        public bool IsDeleted { get; set; }

        public RoleType Role { get; set; }
    }

    public enum RoleType
    {
        Admin = 1,
        Regular = 2
    }
}
