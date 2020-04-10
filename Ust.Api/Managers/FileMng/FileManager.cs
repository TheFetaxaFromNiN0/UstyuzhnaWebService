using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ust.Api.Common;
using Ust.Api.Managers.MetaDataInfoMng;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Response;
using File = Ust.Api.Models.ModelDbObject.File;

namespace Ust.Api.Managers.FileMng
{
    public class FileManager : IFileManager
    {
        private readonly IMetaDataInfoManager metaDataInfoManager;

        public FileManager(IMetaDataInfoManager metaDataInfoManager)
        {
            this.metaDataInfoManager = metaDataInfoManager;
        }

        public async Task<int> SaveFileAsync(ApplicationContext db, IFormFile file, User user, string madeBy, int metaObjectId, int recordId)
        {
            var metaObject = metaDataInfoManager.GetMetaDataInfoById(db, metaObjectId);
            if (metaObject == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                var fileDb = new File
                {
                    Name = file.FileName,
                    ContentType = file.ContentType,
                    DataBytes = binaryReader.ReadBytes((int) file.Length),
                    User = user,
                    CreatedBy = user?.UserName,
                    MadeBy = madeBy,
                    MetaDataInfoId = metaObjectId,
                    MetaDataObjectId = recordId
                };

                await db.Files.AddAsync(fileDb);
                await db.SaveChangesAsync();
                return fileDb.Id;
            }
        }

        public async Task SaveFilesAsync(ApplicationContext db, IList<IFormFile> files, User user, string madeBy, int metaObjectId, int recordId)
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

        public FileResponse GetFile(ApplicationContext db, int fileId)
        {
            var file = db.Files.FirstOrDefault(f => f.Id == fileId);
            if (file == null)
            {
                throw new UstApplicationException(ErrorCode.FileNotFound);
            }

            return new FileResponse
            {
                Data = file.DataBytes,
                ContentType = file.ContentType
            };
        }
    }
}
