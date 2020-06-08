﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Common;
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
                CreatedBy = user.UserName,
                IsDeleted = false
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

            if (news == null || news.IsDeleted)
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
                CreatedDate = news.CreatedDate.ToString("dd.MM.yyyy HH:mm"),
                NewsType = news.NewsType,
                Title = news.Title,
                Text = news.Text,
                Attachments = attachments
            };
        }

        public async Task<IList<NewsSlim>> GetNewsAsync(ApplicationContext db, int skip, int take)
        {
            var news = await db.News.Where(n => n.IsDeleted == false).OrderByDescending(ns => ns.CreatedDate).Skip(skip).Take(take).ToListAsync();

            var newsSlim = news.Select(n => new NewsSlim
            {
                Id = n.Id,
                CreatedBy = n.CreatedBy,
                CreatedDate = n.CreatedDate.ToString("dd.MM.yyyy HH:mm"),
                NewsType = n.NewsType,
                Title = n.Title
            }).ToList();

            return newsSlim;
        }

        public async Task<IList<NewsSlim>> GetNewsByTypeAsync(ApplicationContext db, int newsType, int skip, int take)
        {
            var news = await db.News.Where(n => n.NewsType == newsType && n.IsDeleted == false).OrderByDescending(ns => ns.CreatedDate).Skip(skip).Take(take).ToListAsync();

            var newsSlim = news.Select(n => new NewsSlim
            {
                Id = n.Id,
                CreatedBy = n.CreatedBy,
                CreatedDate = n.CreatedDate.ToString("dd.MM.yyyy HH:mm"),
                NewsType = n.NewsType,
                Title = n.Title
            }).ToList();

            return newsSlim;
        }

        public async Task DeleteNewsByIdAsync(ApplicationContext db, int id)
        {
            var news = await db.News.FindAsync(id);
            if (news == null || news.IsDeleted)
            {
                throw new UstApplicationException(ErrorCode.NewsNotFound);
            }

            news.IsDeleted = true;
            await db.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync(ApplicationContext db, int newsType)
        {
            var count = newsType == 0 ? await db.News.Where(n => n.IsDeleted == false).CountAsync() : await db.News.Where(n => n.NewsType == newsType && n.IsDeleted == false).CountAsync();

            return count;
        }
    }
}
