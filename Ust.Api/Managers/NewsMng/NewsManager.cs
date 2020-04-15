using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Ust.Api.Common;
using Ust.Api.Managers.FileMng;
using Ust.Api.Managers.MetaDataInfoMng;
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
            var newsDb = db.News.Find(id);
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
            var news = db.News.Find(id);

            if (news == null)
            {
                throw new UstApplicationException(ErrorCode.NewsNotFound);
            }

            var metaObjectId = db.MetaDataInfo.FirstOrDefault(m => m.TableName == "News")?.Id;
            if (metaObjectId == null)
            {
                throw new UstApplicationException(ErrorCode.NewsNotFound);
            }

            var files = db.Files.Where(f => f.MetaDataInfoId == metaObjectId && f.MetaDataObjectId == id).ToList();

            var attachments = files.Select(f => new Attachment
            {
                Id = f.Id,
                Name = f.Name
            }).ToList();


            return new NewsPopup
            {
                CreatedBy = news.CreatedBy,
                CreatedDate = news.CreatedDate,
                NewsType = news.NewsType,
                Title = news.Title,
                Text = news.Text,
                Attachments = attachments
            };
        }

        public IList<NewsSlim> GetNews(ApplicationContext db, int skip, int take)
        {
            var news = db.News.Skip(skip).Take(take).ToList();

            var newsSlim = news.Select(n => new NewsSlim
            {
                Id = n.Id,
                CreatedBy = n.CreatedBy,
                CreatedDate = n.CreatedDate,
                NewsType = n.NewsType,
                Title = n.Title
            }).ToList();

            return newsSlim.OrderByDescending(ns => ns.CreatedDate).ToList();
        }

        public IList<NewsSlim> GetNewsByType(ApplicationContext db, int newsType, int skip, int take)
        {
            var news = db.News.Where(n => n.NewsType == newsType).Skip(skip).Take(take);

            var newsSlim = news.Select(n => new NewsSlim
            {
                Id = n.Id,
                CreatedBy = n.CreatedBy,
                CreatedDate = n.CreatedDate,
                NewsType = n.NewsType,
                Title = n.Title
            }).ToList();

            return newsSlim.OrderByDescending(ns => ns.CreatedDate).ToList();
        }       
    }
}
