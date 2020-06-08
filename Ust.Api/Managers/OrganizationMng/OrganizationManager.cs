using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Common;
using Ust.Api.Models.ModelDbObject;
using Ust.Api.Models.Request;
using Ust.Api.Models.Views;

namespace Ust.Api.Managers.OrganizationMng
{
    public class OrganizationManager : IOrganizationManager
    {
        public async Task<int> CreateOrganizationAsync(ApplicationContext db, CreateOrganizationRequest request, User user)
        {
            var newOrg = new Organization
            {
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                Telephones = request.Telephones,
                OrganizationType = request.OrganizationType,
                CreatedBy = user.UserName,
                CreatedDate = DateTimeOffset.Now
            };

            db.Organizations.Add(newOrg);
            await db.SaveChangesAsync();

            return newOrg.Id;
        }

        public async Task<IList<OrganizationSlim>> GetOrganizationByTypeAsync(ApplicationContext db,
            int skip, int take, int orgType = 0)
        {
            var orgs = orgType == 0
                ? await db.Organizations.OrderByDescending(o => o.Name).Skip(skip).Take(take).ToListAsync()
                : await db.Organizations.Where(o => o.OrganizationType.Contains(orgType)).OrderByDescending(o => o.Name).Skip(skip).Take(take).ToListAsync();

            var orgSlim = orgs.Select(o => new OrganizationSlim
            {
                Id = o.Id,
                Name = o.Name,
                CreatedDate = o.CreatedDate.ToString("dd.MM.yyyy HH:mm"),
                CreatedBy = o.CreatedBy,
                Logo = new Logo
                {
                    Id = o.CompanyLogoId,
                    Name = db.CompanyLogos.Find(o.CompanyLogoId).LogoName
                }
            }).ToList();

            return orgSlim;
        }

        public async Task<OrganizationPopUp> GetOrganizationPopUpAsync(ApplicationContext db, int id)
        {
            var org = await db.Organizations.FindAsync(id);
            if (org == null)
                throw new UstApplicationException(ErrorCode.OrganizationNotFound);

            var metaInfoId = db.MetaDataInfo.FirstOrDefault(m => m.TableName == "Organization")?.Id;
            if (metaInfoId == null)
                throw new UstApplicationException(ErrorCode.MetaObjectNotFound);

            var files = db.Files.Where(f => f.MetaDataInfoId == metaInfoId && f.MetaDataObjectId == id).ToList();

            return new OrganizationPopUp
            {
                Id = org.Id,
                Name = org.Name,
                CreatedDate = org.CreatedDate.ToString("dd.MM.yyyy HH:mm"),
                CreatedBy = org.CreatedBy,
                Telephones = org.Telephones,
                Address = org.Address,
                Logo = new Logo
                {
                    Id = org.CompanyLogoId,
                    Name = db.CompanyLogos.Find(org.CompanyLogoId).LogoName
                },
                Attachments = files.Select(f => new Attachment
                {
                    Type = "File",
                    Id = f.Id,
                    Name = f.Name
                }).ToList()
            };
        }

        public async Task<int> GetCountByTypeAsync(ApplicationContext db, int skip, int take,
            int orgType = 0)
        {
            var count = orgType == 0
                ? await db.Organizations.CountAsync()
                : await db.Organizations.Where(o => o.OrganizationType.Contains(orgType)).CountAsync();

            return count;
        }

        public async Task DeleteOrganizationAsync(ApplicationContext db, int id)
        {
            var org = await db.Organizations.FindAsync(id);
            if (org == null)
                throw new UstApplicationException(ErrorCode.OrganizationNotFound);

            org.IsDeleted = true;
            await db.SaveChangesAsync();
        }
    }
}
