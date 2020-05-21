using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ust.Api.Common;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.GalleryMng
{
    public class GalleryManager : IGalleryManager
    {
        public async Task<int> CreateAlbumAsync(ApplicationContext db, CreateAlbumRequest request, User user)
        {
            var newAlbum = new Album
            {
                Name = request.Name,
                ThemeId = request.ThemeId,
                LastDownloadDate = DateTimeOffset.Now,
                TotalPhotoCount = 0,
                Rating = 0,
                ViewCount = 0,
                RewiewCount = 0,
                CreatedBy = user.UserName,
                UserId = user.Id
            };

            db.Add(newAlbum);
            await db.SaveChangesAsync();

            return newAlbum.Id;
        }

        public async Task<int> SaveImageToAlbumAsync(ApplicationContext db, SaveImageRequest request, User user)
        {
            var newImage = new AlbumPhoto
            {
                Name = request.ImageName,
                TakenDate = request.TakenDate,
                TakenBy = request.TakenBy,
                CreatedDate = DateTimeOffset.Now,
                Rating = 0,
                ViewsCount = 0,
                CreatedBy = user.UserName,
                AlbumId = request.AlbumId
            };

            db.AlbumPhoto.Add(newImage);
            await db.SaveChangesAsync();

            var currentAlbum = db.Albums.Find(newImage.AlbumId);
            if (currentAlbum == null) 
            {
                throw new UstApplicationException(ErrorCode.AlbumNotFound);
            }

            currentAlbum.TotalPhotoCount++;

            await db.SaveChangesAsync();

            return newImage.Id;
        }

        public async Task<int> CreateAlbumThemeAsync(ApplicationContext db, string name)
        {
            var newTheme = new AlbumTheme
            {
                Name = name
            };

            db.AlbumThemes.Add(newTheme);
            await db.SaveChangesAsync();

            return newTheme.Id;
        }

        public async Task<IList<AlbumSlim>> GetAlbumsAsync(ApplicationContext db, int skip, int take)
        {
            var albums = db.Albums.OrderByDescending(a => a.LastDownloadDate).Skip(skip).Take(take).ToList();

            return await GetAlbumSlimsAsync(db, albums);

        }

        private async Task<IList<AlbumSlim>> GetAlbumSlimsAsync(ApplicationContext db, IReadOnlyCollection<Album> albums)
        {
            var albumSlims = new List<AlbumSlim>();

            var metaObjectId = (await db.MetaDataInfo.FirstOrDefaultAsync(m => m.TableName == "AlbumPhoto"))?.Id;
            if (metaObjectId == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }

            foreach (var album in albums)
            {
                var image = db.AlbumPhoto.FirstOrDefault(i => i.AlbumId == album.Id);

                var file = db.Files.FirstOrDefault(f =>
                    f.MetaDataInfoId == metaObjectId && f.MetaDataObjectId == image.Id);

                if (image != null && file != null)
                {
                    albumSlims.Add(new AlbumSlim
                    {
                        Id = album.Id,
                        Name = album.Name,
                        ThemeId = album.ThemeId,
                        ThemeName = db.AlbumThemes.Find(album.ThemeId).Name,
                        LastDownloadDate = album.LastDownloadDate.ToString("dd.MM.yyyy HH:mm"),
                        TotalPhotoCount = album.TotalPhotoCount,
                        Rating = album.Rating,
                        ViewCount = album.ViewCount,
                        RewiewCount = album.RewiewCount,
                        CreatedBy = album.CreatedBy,
                        Attachment = new Attachment
                        {
                            Type = "File",
                            Id = file.Id,
                            Name = file.Name
                        }
                    });
                }
                else
                {

                    albumSlims.Add(new AlbumSlim
                    {
                        Id = album.Id,
                        Name = album.Name,
                        ThemeId = album.ThemeId,
                        ThemeName = album.Theme.Name,
                        LastDownloadDate = album.LastDownloadDate.ToString("dd.MM.yyyy HH:mm"),
                        TotalPhotoCount = album.TotalPhotoCount,
                        Rating = album.Rating,
                        ViewCount = album.ViewCount,
                        RewiewCount = album.RewiewCount,
                        CreatedBy = album.CreatedBy,
                    });
                }
            }

            return albumSlims;
        }
    }
}
