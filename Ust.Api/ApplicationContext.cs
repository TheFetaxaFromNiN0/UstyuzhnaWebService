using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Ust.Api.Models;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api
{
    public class ApplicationContext: IdentityDbContext<User>
    {
        private readonly IConfiguration configuration;

        public DbSet<File> Files { get; set; }
        public DbSet<Afisha> Afisha { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<Organization> Organizations { get; set; }


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
            base.OnModelCreating(modelBuilder);


            //Afisha
            modelBuilder.Entity<Afisha>().HasKey(a => a.Id);
            modelBuilder.Entity<File>().Property(a => a.CreatedDate).HasDefaultValueSql("clock_timestamp()");

            //Album
            modelBuilder.Entity<Album>().HasIndex(a => a.Name).IsUnique();
            modelBuilder.Entity<File>().Property(a => a.CreatedDate).HasDefaultValueSql("clock_timestamp()");
            //File
            modelBuilder.Entity<File>().HasKey(f => new {f.Id});
            modelBuilder.Entity<File>().Property(f => f.CreatedDate).HasDefaultValueSql("clock_timestamp()");
            //Для каждой Сущности у которой файлы = таблицу связи

        }




        private string GetConnectionString()
        {
            return configuration.GetConnectionString("ConnectionString");
        }

        
    }
}
