﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Ust.Api.Common;
using Ust.Api.Common.SignalR;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.AfishaMng
{
    public class AfishaManager : IAfishaManager
    {
        public async Task<IList<AfishaSlim>> GetListAsync(ApplicationContext db, int skip, int take)
        {
            var afishies = await db.Afisha.Where(a => a.IsDeleted == false).OrderByDescending(a => a.CreatedDate).Skip(skip).Take(take).ToListAsync();
            var afishiesSlim = new List<AfishaSlim>();

            var metaObjectId = db.MetaDataInfo.FirstOrDefault(m => m.TableName == "Afisha")?.Id;
            if (metaObjectId == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }

            foreach (var afisha in afishies)
            {
                var file = db.Files.FirstOrDefault(f =>
                    f.MetaDataInfoId == metaObjectId && f.MetaDataObjectId == afisha.Id);

                if (file != null)
                {
                    afishiesSlim.Add(new AfishaSlim
                    {
                        Id = afisha.Id,
                        Title = afisha.Title,
                        CreatedDate = afisha.CreatedDate.ToString("dd.MM.yyyy HH:mm"),
                        CreatedBy = afisha.CreatedBy,
                        Attachment = new Attachment
                        {
                            Type = "File",
                            Id = file.Id,
                            Name = file.Name
                        }
                    });
                }
                else
                {
                    afishiesSlim.Add(new AfishaSlim
                    {
                        Id = afisha.Id,
                        Title = afisha.Title,
                        CreatedDate = afisha.CreatedDate.ToString("dd.MM.yyyy HH:mm"),
                        CreatedBy = afisha.CreatedBy
                    });
                }
            }

            var result = afishiesSlim;

            return result;
        }

        public async Task<AfishaPopup> GetAfishaPopupAsync(ApplicationContext db, int id)
        {
            var afisha = await db.Afisha.FindAsync(id);

            if (afisha == null)
            {
                throw new UstApplicationException(ErrorCode.AfishaNotFound);
            }

            var metaObjectId = db.MetaDataInfo.FirstOrDefault(m => m.TableName == "Afisha")?.Id;
            if (metaObjectId == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }

            var files = db.Files.Where(f => f.MetaDataInfoId == metaObjectId && f.MetaDataObjectId == id).ToList();

            var attachments = files.Select(f => new Attachment
            {
                Type = "File",
                Id = f.Id,
                Name = f.Name
            }).ToList();

            return new AfishaPopup
            {
                Id = afisha.Id,
                Title = afisha.Title,
                Description = afisha.Description,
                CreatedBy = afisha.CreatedBy,
                CreatedDate = afisha.CreatedDate.ToString("dd.MM.yyyy HH:mm"),
                Attachments = attachments
            };
        }

        public async Task<int> CreateAfishaAsync(ApplicationContext db, CreateAfishaRequest request, User user)
        {
            var afisha = new Afisha
            {
                Title = request.Title,
                Description = request.Text,
                CreatedBy = user.UserName,
                IsDeleted = false
            };

            await db.AddAsync(afisha);
            await db.SaveChangesAsync();
            return afisha.Id;
        }

        public async Task DeleteAfishaByIdAsync(ApplicationContext db, int id)
        {
            var afisha = await db.Afisha.FindAsync(id);
            if (afisha == null)
            {
                throw new UstApplicationException(ErrorCode.AfishaNotFound);
            }

            afisha.IsDeleted = true;
           
            await db.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync(ApplicationContext db)
        {
            return await db.Afisha.Where(a => a.IsDeleted == false).CountAsync();
        }
    }
}
