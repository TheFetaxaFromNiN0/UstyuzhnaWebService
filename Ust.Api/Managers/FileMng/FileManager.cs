using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ust.Api.Models.ModelDbObject;
using File = Ust.Api.Models.ModelDbObject.File;

namespace Ust.Api.Managers.FileMng
{
    public class FileManager : IFileManager
    {

        public async Task<int> SaveFileAsync(ApplicationContext db, IFormFile file, User user, string madeBy)
        {
            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                var fileDb = new File
                {
                    Name = file.FileName,
                    ContentType = file.ContentType,
                    DataBytes = binaryReader.ReadBytes((int) file.Length),
                    User = user,
                    CreatedBy = user.UserName,
                    MadeBy = madeBy
                };

                await db.Files.AddAsync(fileDb);
                await db.SaveChangesAsync();
                return fileDb.Id;
            }
        }

        public async Task SaveFilesAsync(ApplicationContext db, IList<IFormFile> files, User user, string madeBy)
        {
            IList<File> filesDb = new List<File>();
            foreach (var file in files)
            {
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    var fileDb = new File
                    {
                        Name = file.FileName,
                        ContentType = file.ContentType,
                        DataBytes = binaryReader.ReadBytes((int) file.Length),
                        User = user,
                        CreatedBy = user.UserName,
                        MadeBy = madeBy
                    };

                    filesDb.Add(fileDb);
                }
            }

            await db.Files.AddRangeAsync(filesDb);
            await db.SaveChangesAsync();
            
        }
    }
}
