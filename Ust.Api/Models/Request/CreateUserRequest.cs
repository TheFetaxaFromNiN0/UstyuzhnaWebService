﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api.Models.Request
{
    public class CreateUserRequest
    {
        public User User { get; set; }
    }
}
