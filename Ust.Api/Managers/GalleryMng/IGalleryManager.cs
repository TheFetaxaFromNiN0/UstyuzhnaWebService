using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.GalleryMng
{
    public interface IGalleryManager
    {
        Task<int> CreateAlbumAsync(ApplicationContext db, CreateAlbumRequest request, User user);

        Task<IList<AlbumSlim>> GetAlbumsAsync(ApplicationContext db, int skip, int take);

        Task<int> SaveImageToAlbumAsync(ApplicationContext db, SaveImageRequest request, User user);

        Task<int> CreateAlbumThemeAsync(ApplicationContext db, string name);

    }
}
