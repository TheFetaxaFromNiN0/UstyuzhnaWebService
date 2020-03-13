using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class News
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public NewsCategoryType[] CategoryType { get; set; }
    }

    public enum NewsCategoryType
    {
        City = 1,
        District = 2,
        Region = 3,
        Russia = 4,
        World = 5,
        Sport = 6
    }
}
