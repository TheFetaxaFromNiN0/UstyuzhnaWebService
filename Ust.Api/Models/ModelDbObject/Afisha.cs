using System;

namespace Ust.Api.Models.ModelDbObject
{
    public class Afisha
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        
        public string Description { get; set; }

        public string CreatedBy { get; set; }
    }
}
