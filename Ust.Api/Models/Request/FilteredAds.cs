﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Request
{
    public class FilteredAds 
    {
        public string Title { get; set; }

        public int? CategoryId { get; set; }

        public int? Status { get; set; }
    }
}
