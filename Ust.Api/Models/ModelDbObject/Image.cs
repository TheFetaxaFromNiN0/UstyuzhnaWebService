using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class File
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public int ParentId { get; set; }

        public FileType FileType { get; set; }
    }


    // тип всех сущностей, где присутствуют файлы (картинки)
    public enum FileType
    {
        News = 1,
        Photo = 2,
        Organization = 3,

    }

}
