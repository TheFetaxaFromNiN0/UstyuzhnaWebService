﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.ModelDbObject
{
    public class BadWord
    {
        [Key]
        public int Id { get; set; }

        public string Word { get; set; }
    }
}