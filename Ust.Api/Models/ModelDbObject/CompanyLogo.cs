using System.ComponentModel.DataAnnotations;

namespace Ust.Api.Models.ModelDbObject
{
    public class CompanyLogo
    {
        [Key]
        public int Id { get; set; }

        public string LogoName { get; set; }

        public byte[] DataBytes { get; set; }

        public Organization Organization { get; set; }
    }
}
