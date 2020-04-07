using System.Threading.Tasks;
using Ust.Api.Models;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api.Common.Auth
{
    public interface IUserContext
    {
        Task<User> GetCurrentUserAsync();
    }
}
