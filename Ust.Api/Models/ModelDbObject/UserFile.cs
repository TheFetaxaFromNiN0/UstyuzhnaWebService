using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;
using VkNet.Model.RequestParams.Polls;

namespace Ust.Api.Models.ModelDbObject
{
    public class UserFile
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("File")]
        public int FileId { get; set; }

        public User User { get; set; }
        public File File { get; set; }
    }
}
