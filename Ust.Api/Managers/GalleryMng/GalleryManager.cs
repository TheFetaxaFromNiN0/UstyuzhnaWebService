using System;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;

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
    }
}
