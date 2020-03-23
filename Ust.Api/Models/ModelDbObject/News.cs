using System;
using System.ComponentModel.DataAnnotations;

namespace Ust.Api.Models.ModelDbObject
{
    public class News
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public NewsType[] NewsType { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }

    public enum NewsType
    {
        City = 1,
        District = 2,
        Region = 3,
        Russia = 4,
        World = 5,
        Sport = 6
    }
}
