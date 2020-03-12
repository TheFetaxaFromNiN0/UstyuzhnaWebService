using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Models
{
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string[] Telephones { get; set; }

        public OrgCategoryType OrgCategoyType { get; set; }
    }

    public enum OrgCategoryType
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
