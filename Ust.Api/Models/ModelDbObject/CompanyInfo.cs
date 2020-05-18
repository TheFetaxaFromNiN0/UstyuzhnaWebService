using System.ComponentModel.DataAnnotations;

namespace Ust.Api.Models.ModelDbObject
{
    public class CompanyInfo
    {
        [Key]
        public int Id { get; set; }

        public string ShortName { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Telephones { get; set; }
    }
}
