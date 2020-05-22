using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ust.Api.Models.ModelDbObject
{
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string[] Telephones { get; set; }

        public int[] OrganizationType { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [ForeignKey("CompanyLogos")]
        public int? CompanyLogoId { get; set; }
        public CompanyLogo CompanyLogo { get; set; }

    }

    public enum OrganizationType
    {
        Auto = 1,
        RealProperty = 2,
        Сonstruction = 3,
        BeautyAndHealth = 4,
        EntertainmentCulture = 5,
        FinanceBusiness = 6,
        Goverment = 7,
        Service = 8,
        Science = 9,
        ForestryManufactures = 10,
        Wholesale = 11,
        Production = 12
    }
}
