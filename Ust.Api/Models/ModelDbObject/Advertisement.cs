using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ust.Api.Models.ModelDbObject
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public bool IsModerate { get; set; }
    }

    // енумы не маппятся - для запоминания номера категории
    public enum CategoryType
    {
        Авто = 1,
        Нежвижимость = 2,
        БытоваяТехника = 3,
        Электроника = 4,
        ОдеждаИОбувь = 5,
        МебельИИнтерьер = 6,
        СадОгород = 7,
        СредствоСвязи = 8,
        Разное = 9,
        Услуги = 10,
        ТоварыДляДетей = 11,
        Животные = 12,
        ПредметыДсуга = 13
    }
}
