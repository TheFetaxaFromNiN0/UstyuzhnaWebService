using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class AfishaFilecs
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Afisha")]
        public int AfishaId { get; set; }
        [ForeignKey("File")]
        public int FileId { get; set; }

        public Afisha Afisha { get; set; }
        public File File { get; set; }
    }
}
