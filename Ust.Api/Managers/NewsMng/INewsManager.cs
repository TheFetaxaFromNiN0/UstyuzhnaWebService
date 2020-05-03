using System.Collections.Generic;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.NewsMng
{
    public interface INewsManager
    {
        Task<int> CreateNewsAsync(ApplicationContext db, CreateNewsRequest request, User user);

        Task UpdateNewsAsync(ApplicationContext db, int id, UpdateNewsRequest request);

        Task<NewsPopup> GetNewsPopupAsync(ApplicationContext db, int id);

        Task<IList<NewsSlim>> GetNewsAsync(ApplicationContext db, int skip, int take);

        Task<IList<NewsSlim>> GetNewsByTypeAsync(ApplicationContext db, int newsType, int skip, int take);

        Task DeleteNewsByIdAsync(ApplicationContext db, int id);


        // Task<IEnumerable<NewsSlim>> GetAllNewsAsync(ApplicationContext db);

        //Task<IEnumerable<NewsSlim>> GetNewsByTypeAsync(ApplicationContext db, NewsType newsType);
    }
}
