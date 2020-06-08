using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace Ust.Api.Models.ModelDbObject
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int NewsType { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public bool IsDeleted { get; set; }
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
