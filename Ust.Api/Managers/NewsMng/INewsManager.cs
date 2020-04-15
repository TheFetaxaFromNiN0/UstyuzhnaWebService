using System.Collections.Generic;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.NewsMng
{
    public interface INewsManager
    {
        Task CreateNewsAsync(ApplicationContext db, CreateNewsRequest request, User user);

        Task UpdateNewsAsync(ApplicationContext db, int id, UpdateNewsRequest request);

        NewsPopup GetNewsPopup(ApplicationContext db, int id);

        IList<NewsSlim> GetNews(ApplicationContext db, int skip, int take);

        IList<NewsSlim> GetNewsByType(ApplicationContext db, int newsType, int skip, int take);


        // Task<IEnumerable<NewsSlim>> GetAllNewsAsync(ApplicationContext db);

        //Task<IEnumerable<NewsSlim>> GetNewsByTypeAsync(ApplicationContext db, NewsType newsType);
    }
}
