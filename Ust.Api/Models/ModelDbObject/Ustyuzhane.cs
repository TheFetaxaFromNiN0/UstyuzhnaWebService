using System;
using System.ComponentModel.DataAnnotations;

namespace Ust.Api.Models.ModelDbObject
{
    public class Ustyuzhane
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
