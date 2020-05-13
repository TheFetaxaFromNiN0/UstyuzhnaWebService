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

        Task<AdsSlimsWithTotal> GetAdsByCategoryAsync(ApplicationContext db, int categoryId, int skip, int take, int status = 3);

        Task<AdsPopup> GetAdsPopupAsync(ApplicationContext db, int id);

        Task SetStatusAsync(ApplicationContext db, List<ModeratedAds> requestList);

        Task<AdsSlimsWithTotal> GetMy(ApplicationContext db, int take, int skip, User user);
    }
}
