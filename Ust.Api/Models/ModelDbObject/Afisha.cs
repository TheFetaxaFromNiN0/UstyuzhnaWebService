using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class Afisha
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public ICollection<AfishaFile> AfishaFiles { get; set; }
    }
}
