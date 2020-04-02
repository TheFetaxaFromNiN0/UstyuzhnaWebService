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

        public DbSet<AfishaFile> AfishaFiles { get; set; }
        public DbSet<AlbumFile> AlbumFiles { get; set; }
        public DbSet<AlbumComment> AlbumComments { get; set; }
        public DbSet<NewsFile> NewsFiles { get; set; }
        public DbSet<NewsComment> NewsComments { get; set; }
        public DbSet<OrganizationFile> OrganizationFiles { get; set; }


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

            //AfishaFile
            modelBuilder.Entity<AfishaFile>()
                .HasOne(af => af.Afisha)
                .WithMany(a => a.AfishaFiles)
                .HasForeignKey(af => af.AfishaId);
            modelBuilder.Entity<AfishaFile>()
                .HasOne(af => af.File)
                .WithMany(f => f.AfishaFiles)
                .HasForeignKey(af => af.FileId);

            //AlbumFile
            modelBuilder.Entity<AlbumFile>()
                .HasOne(af => af.Album)
                .WithMany(a => a.AlbumFiles)
                .HasForeignKey(af => af.AlbumId);
            modelBuilder.Entity<AlbumFile>()
                .HasOne(af => af.File)
                .WithMany(a => a.AlbumFiles)
                .HasForeignKey(af => af.FileId);

            //AlbumComment
            modelBuilder.Entity<AlbumComment>()
                .HasOne(ac => ac.Album)
                .WithMany(a => a.AlbumComments)
                .HasForeignKey(ac => ac.AlbumId);
            modelBuilder.Entity<AlbumComment>()
                .HasOne(ac => ac.Comment)
                .WithMany(a => a.AlbumComments)
                .HasForeignKey(ac => ac.CommentId);

            //NewsFile
            modelBuilder.Entity<NewsFile>()
                .HasOne(nf => nf.News)
                .WithMany(n => n.NewsFiles)
                .HasForeignKey(nf => nf.NewsId);
            modelBuilder.Entity<NewsFile>()
                .HasOne(nf => nf.File)
                .WithMany(f => f.NewsFiles)
                .HasForeignKey(nf => nf.FileId);

            //NewsComment
            modelBuilder.Entity<NewsComment>()
                .HasOne(nc => nc.News)
                .WithMany(n => n.NewsComments)
                .HasForeignKey(nc => nc.NewsId);
            modelBuilder.Entity<NewsComment>()
                .HasOne(nc => nc.Comment)
                .WithMany(c => c.NewsComments)
                .HasForeignKey(nc => nc.CommentId);

            //OrganizationFile
            modelBuilder.Entity<OrganizationFile>()
                .HasOne(of => of.Organization)
                .WithMany(o => o.OrganizationFiles)
                .HasForeignKey(of => of.OrganizationId);
            modelBuilder.Entity<OrganizationFile>()
                .HasOne(of => of.File)
                .WithMany(f => f.OrganizationFiles)
                .HasForeignKey(of => of.FileId);

            //Album
            modelBuilder.Entity<Album>().HasIndex(a => a.Name).IsUnique();
            modelBuilder.Entity<File>().Property(a => a.CreatedDate).HasDefaultValueSql("clock_timestamp()");

            //File
            modelBuilder.Entity<File>().HasKey(f => new {f.Id});
            modelBuilder.Entity<File>().Property(f => f.CreatedDate).HasDefaultValueSql("clock_timestamp()");

            //User
            modelBuilder.Entity<CommentHistory>()
                .HasOne(ch => ch.User)
                .WithMany(u => u.Comments);
        }




        private string GetConnectionString()
        {
            return configuration.GetConnectionString("ConnectionString");
        }

        
    }
}
