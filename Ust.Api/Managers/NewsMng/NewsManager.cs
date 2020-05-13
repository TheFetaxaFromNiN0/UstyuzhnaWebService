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
        public async Task<int> CreateNewsAsync(ApplicationContext db, CreateNewsRequest request, User user)
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
            return news.Id;
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

        public async Task<NewsPopup> GetNewsPopupAsync(ApplicationContext db, int id)
        {
            var news = await db.News.FindAsync(id);

            if (news == null)
            {
                throw new UstApplicationException(ErrorCode.NewsNotFound);
            }

            var metaObjectId = db.MetaDataInfo.FirstOrDefault(m => m.TableName == "News")?.Id;
            if (metaObjectId == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }

            var files = db.Files.Where(f => f.MetaDataInfoId == metaObjectId && f.MetaDataObjectId == id).ToList();

            var attachments = files.Select(f => new Attachment
            {
                Type = "File",
                Id = f.Id,
                Name = f.Name
            }).ToList();


            return new NewsPopup
            {
                Id = news.Id,
                CreatedBy = news.CreatedBy,
                CreatedDate = news.CreatedDate,
                NewsType = news.NewsType,
                Title = news.Title,
                Text = news.Text,
                Attachments = attachments
            };
        }

        public async Task<NewsSlimwithTotal> GetNewsAsync(ApplicationContext db, int skip, int take)
        {
            var news = await db.News.OrderByDescending(ns => ns.CreatedDate).Skip(skip).Take(take).ToListAsync();

            var total = db.News.Count();

            var newsSlim = news.Select(n => new NewsSlim
            {
                Id = n.Id,
                CreatedBy = n.CreatedBy,
                CreatedDate = n.CreatedDate,
                NewsType = n.NewsType,
                Title = n.Title
            }).ToList();

            return new NewsSlimwithTotal
            {
                NewsSlims = newsSlim,
                Total = total
            };
        }

        public async Task<NewsSlimwithTotal> GetNewsByTypeAsync(ApplicationContext db, int newsType, int skip, int take)
        {
            var news = await db.News.Where(n => n.NewsType == newsType).OrderByDescending(ns => ns.CreatedDate).Skip(skip).Take(take).ToListAsync();

            var total = db.News.Count(n => n.NewsType == newsType);

            var newsSlim = news.Select(n => new NewsSlim
            {
                Id = n.Id,
                CreatedBy = n.CreatedBy,
                CreatedDate = n.CreatedDate,
                NewsType = n.NewsType,
                Title = n.Title
            }).ToList();

            return new NewsSlimwithTotal
            {
                NewsSlims = newsSlim,
                Total = total
            };
        }

        public async Task DeleteNewsByIdAsync(ApplicationContext db, int id)
        {
            var news = await db.News.FindAsync(id);
            if (news == null)
            {
                throw new UstApplicationException(ErrorCode.NewsNotFound);
            }

            var metaInfo = await db.MetaDataInfo.FirstOrDefaultAsync(m => m.TableName == "News");
            if (metaInfo == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }

            var attachments = db.Files.Where(att => att.MetaDataInfoId == metaInfo.Id && id == att.MetaDataObjectId);
            db.Files.RemoveRange(attachments);
            db.News.Remove(news);
            await db.SaveChangesAsync();
        }
    }
}
