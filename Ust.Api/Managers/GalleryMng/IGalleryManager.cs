using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;

namespace Ust.Api.Managers.GalleryMng
{
    public interface IGalleryManager
    {
        Task<int> CreateAlbumAsync(ApplicationContext db, CreateAlbumRequest request, User user);
    }
}
