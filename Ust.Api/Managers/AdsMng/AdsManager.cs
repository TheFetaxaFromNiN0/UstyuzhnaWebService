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

        public async Task<IList<AdsSlim>> GetAdsByFilterAsync(ApplicationContext db, FilteredAds filter, int take, int skip)
        {
           throw new NotImplementedException();

        }

        public async Task<IList<AdsSlim>> GetAdsByCategoryAsync(ApplicationContext db, int categoryId, int skip, int take, int status = 3) //добавить проверку на статус
        {
            var ads = categoryId == 0 ? db.Advertisements.Where(a => a.Status == status).OrderByDescending(a => a.CreatedDate).Skip(skip).Take(take).ToList()
                : db.Advertisements.Where(ad => ad.CategoryId == categoryId && ad.Status == status).OrderByDescending(a => a.CreatedDate).Skip(skip).Take(take).ToList();

            return await GetAdsSlimsAsync(db, ads);
        }

        public async Task<IList<AdsSlim>> GetMy(ApplicationContext db, int skip, int take, User user)
        {
            var myAds = db.Advertisements.Where(ad => ad.User == user).OrderByDescending(a => a.CreatedDate).Skip(skip).Take(take).ToList();

            return await GetAdsSlimsAsync(db, myAds);
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

        public async Task SetStatusAsync(ApplicationContext db, List<ModeratedAds> requestList)
        {
            foreach (var ad in requestList)
            {
                var advertisements = db.Advertisements.Find(ad.AdId);
                advertisements.Status = ad.Status;
            }

            await db.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync(ApplicationContext db, int categoryId)
        {
            var count = categoryId == 0 ? await db.Advertisements.CountAsync() : await db.Advertisements.Where(a => a.CategoryId == categoryId).CountAsync();

            return count;
        }

        public async Task<int> GetMyAdsCountAsync(ApplicationContext db, User user)
        {
            return await db.Advertisements.Where(a => a.User == user).CountAsync();
        }

        private async Task<IList<AdsSlim>> GetAdsSlimsAsync(ApplicationContext db, IReadOnlyCollection<Advertisement> ads)
        {
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
                        },
                        Status = ad.Status,
                        CategoryId = ad.CategoryId
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
                        CategoryId = ad.CategoryId,
                        Status = ad.Status
                    });
                }
            }

            return adsSlim;
        }
    }
}
