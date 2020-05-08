using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ust.Api.Common;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.AdsMng
{
    public class AdsManager : IAdsManager
    {
        public async Task<int> CreateAdsAsync(ApplicationContext db, CreateAdsRequest request, User user)
        {
            var newAd = new Advertisement
            {
                Title = request.Title,
                CategoryId = request.CategoryId,
                Description = request.Description,
                ContactName = request.ContactName,
                ContactPhone = request.ContactPhone,
                ContactEmail = request.Email,
                UserId = user?.Id,
                Status = 1,
                Createdby = user?.UserName
            };

            db.Add(newAd);
            await db.SaveChangesAsync();

            return newAd.Id;
        }

        public async Task<IList<AdsSlim>> GetAdsByCategoryAsync(ApplicationContext db, int categoryId, int skip, int take)
        {
            List<Advertisement> ads;
            ads = categoryId == 0 ? db.Advertisements.Skip(skip).Take(take).ToList() : db.Advertisements.Where(ad => ad.CategoryId == categoryId).Skip(skip).Take(take).ToList();

            var adsSlim = new List<AdsSlim>();

            var metaObjectId = (await db.MetaDataInfo.FirstOrDefaultAsync(m => m.TableName == "Ads"))?.Id;
            if (metaObjectId == null)
            {
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);
            }

            foreach (var ad in ads)
            {
                var file = db.Files.FirstOrDefault(f =>
                    f.MetaDataInfoId == metaObjectId && f.MetaDataObjectId == ad.Id);

                if (file != null)
                {

                    adsSlim.Add(new AdsSlim
                    {
                        Id = ad.Id,
                        Title = ad.Title,
                        CreatedDate = ad.CreatedDate,
                        CreatedBy = ad.Createdby,
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

                    adsSlim.Add(new AdsSlim
                    {
                        Id = ad.Id,
                        Title = ad.Title,
                        CreatedDate = ad.CreatedDate,
                        CreatedBy = ad.Createdby,
                        CategoryId = ad.CategoryId
                    });
                }
            }

            return adsSlim.OrderByDescending(ad => ad.CreatedDate).ToList();
        }

        public async Task<AdsPopup> GetAdsPopupAsync(ApplicationContext db, int id)
        {
            var ad = await db.Advertisements.FindAsync(id);

            if (ad == null)
            {
                throw new UstApplicationException(ErrorCode.AdNotFound);
            }

            var metaObjectId = db.MetaDataInfo.FirstOrDefault(m => m.TableName == "Ads")?.Id;
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

            return new AdsPopup
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description,
                CreatedBy = ad.Createdby,
                CreatedDate = ad.CreatedDate,
                ContactPhone = ad.ContactPhone,
                ContactEmail = ad.ContactEmail,
                ContactName = ad.ContactName,
                CategoryId = ad.CategoryId,
                Attachments = attachments
            };
        }
    }
}
