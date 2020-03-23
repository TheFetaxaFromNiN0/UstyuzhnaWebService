﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class AlbumFile
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Album")]
        public int AlbumId { get; set; }
        [ForeignKey("File")]
        public int FileId { get; set; }

        public Album Album { get; set; }
        public File File { get; set; }
    }
}