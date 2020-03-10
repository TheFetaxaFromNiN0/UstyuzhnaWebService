using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Models.Request;

namespace Ust.Api.BizRules.User
{
    public interface IUserBizRules
    {
        Task<int> CreateUserAsync(CreateUserRequest request);
    }
}
