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
            //Afisha
            modelBuilder.Entity<Afisha>().HasKey(a => a.Id);
            modelBuilder.Entity<File>().Property(a => a.CreatedDate).HasDefaultValueSql("getdate()");

            //Album
            modelBuilder.Entity<Album>().HasIndex(a => a.Name).IsUnique();
            modelBuilder.Entity<File>().Property(a => a.CreatedDate).HasDefaultValueSql("getdate()");
            //Для каждой Сущности у которой файлы = таблицу связи
            //File
            modelBuilder.Entity<File>().HasKey(f => new {f.Id});
            modelBuilder.Entity<File>().Property(f => f.CreatedDate).HasDefaultValueSql("getdate()");

        }




        private string GetConnectionString()
        {
            return configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        
    }
}
