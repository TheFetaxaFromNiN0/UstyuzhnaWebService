﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ust.Api.Models.ModelDbObject
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public string MadeBy { get; set; }

        public AlbumCategory AlbumCategory { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public ICollection<AlbumFile> AlbumFiles { get; set; }

        public ICollection<AlbumComment> AlbumComments { get; set; }
    }

    public enum AlbumCategory
    {

    }
}
