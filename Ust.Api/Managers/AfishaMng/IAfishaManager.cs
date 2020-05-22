using System.Collections.Generic;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.AfishaMng
{
    public interface IAfishaManager
    {
        Task<IList<AfishaSlim>> GetListAsync(ApplicationContext db, int skip, int take);

        Task<AfishaPopup> GetAfishaPopupAsync(ApplicationContext db, int id, string connectionId);

        Task<int> CreateAfishaAsync(ApplicationContext db, CreateAfishaRequest request, User user);

        Task DeleteAfishaByIdAsync(ApplicationContext db, int id);

        Task<int> GetCountAsync(ApplicationContext db);
    }
}
