using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.AdsMng
{
    public interface IAdsManager
    {
        Task<int> CreateAdsAsync(ApplicationContext db, CreateAdsRequest request, User user);

        Task<IList<AdsSlim>> GetAdsByCategoryAsync(ApplicationContext db, int categoryId, int skip, int take);

        Task<AdsPopup> GetAdsPopupAsync(ApplicationContext db, int id);


    }
}
