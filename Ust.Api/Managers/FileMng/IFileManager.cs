using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api.Managers.FileMng
{
    public interface IFileManager
    {
        Task<int> SaveFileAsync(ApplicationContext db, IFormFile file, User user, string madeBy);

        Task SaveFilesAsync(ApplicationContext db, IList<IFormFile> files, User user, string madeBy);
    }
}
