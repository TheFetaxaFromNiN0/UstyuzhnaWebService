using System.ComponentModel.DataAnnotations;

namespace Ust.Api.Models.ModelDbObject
{
    public class AlbumTheme
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
