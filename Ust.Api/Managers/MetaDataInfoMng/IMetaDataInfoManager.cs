using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Response;

namespace Ust.Api.Managers.MetaDataInfoMng
{
    public interface IMetaDataInfoManager
    {
        Task SaveMetaDataAsync(ApplicationContext db, CreateMetaInfoRequest request);

        Task UpdateMetaDataAsync(ApplicationContext db, UpdateMetaInfoRequest request);

        Task DeleteMetaDataAsync(ApplicationContext db, int id);

        Task<List<MetaDataInfo>> GetMetaDataAsync(ApplicationContext db);

        Task<MetaDataInfo> GetMetaDataInfoByIdAsync(ApplicationContext db, int id);

        Task<string> GetTypeByMetaDataInfoAsync(ApplicationContext db, int id);

        Task<MetaDataInfo> GetMetaDataInfoByNameAsync(ApplicationContext db, string tableName);

        Task<HasAttachmentAndComments> GetFlagsAsync(ApplicationContext db, string tableName);
    }
}
