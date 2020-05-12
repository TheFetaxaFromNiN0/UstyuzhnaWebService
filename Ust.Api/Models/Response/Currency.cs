using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models.Response
{
    public class Currency
    {
        public string CurrentDate { get; set; }

        public decimal Dollar { get; set; }

        public decimal Euro { get; set; }
    }

    public class Valute
    {
        public decimal Value { get; set; }
    }

}
