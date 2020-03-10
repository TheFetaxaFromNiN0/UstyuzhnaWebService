using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Models;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api
{
    public class ApplicationContext: DbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }


        public ApplicationContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }




        private string GetConnectionString()
        {
            return configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        
    }
}
