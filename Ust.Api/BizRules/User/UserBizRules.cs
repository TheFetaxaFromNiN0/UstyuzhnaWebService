using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Models.Request;

namespace Ust.Api.BizRules.User
{
    public class UserBizRules: IUserBizRules
    {
       public Task<int> CreateUserAsync(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
