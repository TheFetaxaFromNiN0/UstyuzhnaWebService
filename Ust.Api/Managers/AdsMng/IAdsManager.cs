using System.Collections.Generic;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.AdsMng
{
    public interface IAdsManager
    {
        Task<int> CreateAdsAsync(ApplicationContext db, CreateAdsRequest request, User user);

        Task<IList<AdsSlim>> GetAdsByCategoryAsync(ApplicationContext db, int categoryId, int skip, int take, int status = 3);

        Task<IList<AdsSlim>> GetAdsByFilterAsync(ApplicationContext db, FilteredAds filter, int take, int skip);

        Task<AdsPopup> GetAdsPopupAsync(ApplicationContext db, int id);

        Task SetStatusAsync(ApplicationContext db, List<ModeratedAds> requestList);

        Task<IList<AdsSlim>> GetMy(ApplicationContext db, int take, int skip, User user);

        Task<int> GetCountAsync(ApplicationContext db, int categoryId);

        Task<int> GetMyAdsCountAsync(ApplicationContext db, User user);
    }
}
