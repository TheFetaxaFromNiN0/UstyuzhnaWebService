using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Response;

namespace Ust.Api.Managers.FileMng
{
    public interface IFileManager
    {
        Task<int> SaveFileAsync(ApplicationContext db, IFormFile file, User user, string madeBy, int metaObjectId,
            int recordId);

        Task SaveFilesAsync(ApplicationContext db, IFormFileCollection files, User user, string madeBy, int metaObjectId,
            int recordId);

        FileResponse GetFile(ApplicationContext db, int fileId);

        Task<int> SaveCompanyLogoAsync(ApplicationContext db, IFormFile file, int orgId);
    }
}
