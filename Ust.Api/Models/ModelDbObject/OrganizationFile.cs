using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class OrganizationFile
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        [ForeignKey("File")]
        public int FileId { get; set; }

        public Organization Organization { get; set; }
        public File File { get; set; }

    }
}
}
