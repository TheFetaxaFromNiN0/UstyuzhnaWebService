using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Common;

namespace Ust.Api.Models.Views
{
    public class OrganizationSlim
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public Logo Logo { get; set; }
    }

    public class OrganizationPopUp
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string Address { get; set; }

        public string[] Telephones { get; set; }

        public IList<Attachment> Attachments { get; set; }

        public Logo Logo { get; set; }
    }

    public class Logo
    {
        public int? Id { get; set; }
        
        public string Name { get; set; }
    }
}
