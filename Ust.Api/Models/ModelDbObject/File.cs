using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class File
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string MadeBy { get; set; }

        public byte[] DataBytes { get; set; }

        public ICollection<AfishaFile> AfishaFiles { get; set; }
        
        public ICollection<AlbumFile> AlbumFiles { get; set; }

        public ICollection<NewsFile> NewsFiles { get; set; }

        public ICollection<OrganizationFile> OrganizationFiles { get; set; }
    }
}
