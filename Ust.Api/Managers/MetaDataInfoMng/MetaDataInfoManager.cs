using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Ust.Api.Common;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;

namespace Ust.Api.Managers.MetaDataInfoMng
{
    public class MetaDataInfoManager: IMetaDataInfoManager
    {

        public async Task SaveMetaDataAsync(ApplicationContext db, CreateMetaInfoRequest request)
        {
            var newMetaDataInfo = new MetaDataInfo
            {
                TableName = request.TableName,
                HasAttachment = request.HasAttachment,
                HasComment = request.HasComment
            };

            await db.MetaDataInfo.AddAsync(newMetaDataInfo);
            await db.SaveChangesAsync();
        }

        public async Task UpdateMetaDataAsync(ApplicationContext db, UpdateMetaInfoRequest request)
        {
            var metaDataInfoDb = db.MetaDataInfo.FirstOrDefault(mdi => mdi.Id == request.Id);

            if (metaDataInfoDb == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }

            metaDataInfoDb.HasAttachment = request.HasAttachment;
            metaDataInfoDb.HasComment = request.HasComment;
            metaDataInfoDb.TableName = request.TableName;

            await db.SaveChangesAsync();
        }

        public async Task DeleteMetaDataAsync(ApplicationContext db, int id)
        {
            var metaDataInfoDb = db.MetaDataInfo.Find(id);
            if (metaDataInfoDb == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }
            await db.SaveChangesAsync();
        }

        public List<MetaDataInfo> GetMetaData(ApplicationContext db)
        {
            return db.MetaDataInfo.ToList();
        }

        public MetaDataInfo GetMetaDataInfoById(ApplicationContext db, int id)
        {
            var result = db.MetaDataInfo.Find(id);

            return result;
        }

        public string GetTypeByMedaDataInfo(ApplicationContext db, int id)
        {
            var metaObject = db.MetaDataInfo.First(m => m.Id == id);
            return metaObject.TableName;
        }

    }
}
