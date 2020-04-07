using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Npgsql;
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
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<MetaDataInfo> MetaDataInfo { get; set; }
        public DbSet<CommentHistory> CommentHistories { get; set; }

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
            modelBuilder.Entity<File>().Property(a => a.CreatedDate).HasDefaultValueSql("now()");
          
            //Album
            modelBuilder.Entity<Album>().HasIndex(a => a.Name).IsUnique();
            modelBuilder.Entity<File>().Property(a => a.CreatedDate).HasDefaultValueSql("now()");

            //File
            modelBuilder.Entity<File>().HasKey(f => new {f.Id});
            modelBuilder.Entity<File>().Property(f => f.CreatedDate).HasDefaultValueSql("now()");

            //News
            modelBuilder.Entity<News>().Property(f => f.CreatedDate).HasDefaultValueSql("now()");
            modelBuilder.Entity<News>().HasIndex(n => n.NewsType);

            //CommentHistory
            modelBuilder.Entity<CommentHistory>().Property(f => f.CreatedDate).HasDefaultValueSql("now()");

            //Organization
            modelBuilder.Entity<Organization>().HasIndex(o => o.OrganizationType);

            //User
            modelBuilder.Entity<CommentHistory>()
                .HasOne(ch => ch.User)
                .WithMany(u => u.Comments);
            modelBuilder.Entity<File>()
                .HasOne(f => f.User)
                .WithMany(u => u.Files);
        }




        private string GetConnectionString()
        {
            return configuration.GetConnectionString("ConnectionString");
        }


    }
}
