using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Views
{
    public class UserView
    {
        public string Name { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string RoleName { get; set; }
    }
}
