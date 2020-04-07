using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Ust.Api.Common;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.NewsMng
{
    public class NewsManager : INewsManager
    {
        public async Task CreateNewsAsync(ApplicationContext db, CreateNewsRequest request, User user)
        {
            var news = new News
            {
                Title = request.Title,
                Text = request.Text,
                NewsType = request.NewsType,
                CreatedBy = user.UserName
            };

            await db.News.AddAsync(news);           
            await db.SaveChangesAsync();
        }

        public async Task UpdateNewsAsync(ApplicationContext db, int id, UpdateNewsRequest request)
        {
            var newsDb = db.News.First(n => n.Id == id);
            if (newsDb == null)
            {
               throw new UstApplicationException(ErrorCode.NewsNotFound);
            }

            newsDb.Text = request.Text;
            newsDb.Title = request.Title;
            newsDb.NewsType = request.NewsType;

            await db.SaveChangesAsync();
        }

        public NewsPopup GetNewsPopup(ApplicationContext db, int id)
        {
            var news = db.News.First(n => n.Id == id);

            if (news == null)
            {
                throw new UstApplicationException(ErrorCode.NewsNotFound);
            }

            return new NewsPopup
            {
                CreatedBy = news.CreatedBy,
                CreatedDate = news.CreatedDate,
                NewsType = news.NewsType,
                Title = news.Title,
                Text = news.Text
            };
        }

    }
}
