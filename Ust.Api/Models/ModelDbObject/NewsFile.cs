using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ust.Api.Models.ModelDbObject
{
    public class NewsFile
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("News")]
        public int NewsId { get; set; }
        [ForeignKey("File")]
        public int FileId { get; set; }

        public News News { get; set; }
        public File File { get; set; }

    }
}
