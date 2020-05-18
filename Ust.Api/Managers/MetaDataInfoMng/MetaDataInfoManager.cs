using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
                RussianTableName = request.RussianTableName,
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
            metaDataInfoDb.RussianTableName = request.RussianTableName;

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

        public async Task<List<MetaDataInfo>> GetMetaDataAsync(ApplicationContext db)
        {
            return await db.MetaDataInfo.ToListAsync();
        }

        public async Task<MetaDataInfo> GetMetaDataInfoByIdAsync(ApplicationContext db, int id)
        {
            var result = await db.MetaDataInfo.FindAsync(id);
            if (result == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }

            return result;
        }

        public async Task<string> GetTypeByMetaDataInfoAsync(ApplicationContext db, int id)
        {
            var metaObject = await db.MetaDataInfo.FirstAsync(m => m.Id == id);
            if (metaObject == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }
            return metaObject.TableName;
        }

        public async Task<MetaDataInfo> GetMetaDataInfoByNameAsync(ApplicationContext db, string tableName)
        {
            var metaObject = await db.MetaDataInfo.FirstAsync(m => m.TableName == tableName);
            if (metaObject == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }
            return metaObject;
        }

    }
}
