using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Request
{
    public class CreateOrganizationRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string[] Telephones { get; set; }

        public int[] OrganizationType { get; set; }
    }
}
