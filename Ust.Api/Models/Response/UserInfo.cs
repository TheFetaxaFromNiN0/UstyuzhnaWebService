using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ust.Api.Models.Response
{
    public class UserInfo
    {
        public UserType UserType { get; set; }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserType
    {
        Anonymous,
        Registered
    }
}
