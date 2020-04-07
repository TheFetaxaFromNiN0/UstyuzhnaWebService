using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;

namespace Ust.Api.Managers.MetaDataInfoMng
{
    public interface IMetaDataInfoManager
    {
        Task SaveMetaDataAsync(ApplicationContext db, CreateMetaInfoRequest request);

        Task UpdateMetaDataAsync(ApplicationContext db, UpdateMetaInfoRequest request);

        Task DeleteMetaDataAsync(ApplicationContext db, int id);

        List<MetaDataInfo> GetMetaData(ApplicationContext db);
    }
}
