using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Common.AdsCommon
{
    public enum Status : byte
    {
        NonModerate = 1,
        NonSuccessModerateByService = 2,
        SuccessModerate = 3,
        NoSuccessByAdmin = 4
    }
}
