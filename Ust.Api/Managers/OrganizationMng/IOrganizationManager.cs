using System.Collections.Generic;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.OrganizationMng
{
    public interface IOrganizationManager
    {
        Task<int> CreateOrganizationAsync(ApplicationContext db, CreateOrganizationRequest request, User user);

        Task<IList<OrganizationSlim>> GetOrganizationByTypeAsync(ApplicationContext db, int skip, int take, int orgType = 0);

        Task<OrganizationPopUp> GetOrganizationPopUpAsync(ApplicationContext db, int id);

        Task<int> GetCountByTypeAsync(ApplicationContext db, int skip, int take, int orgType = 0);

        Task DeleteOrganizationAsync(ApplicationContext db, int id);
    }
}
