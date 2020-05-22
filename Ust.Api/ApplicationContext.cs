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
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<MetaDataInfo> MetaDataInfo { get; set; }
        public DbSet<CommentHistory> CommentHistories { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumPhoto> AlbumPhoto { get; set; }
        public DbSet<HistoryArticle> HistoryArticles { get; set; }
        public DbSet<Ustyuzhane> Ustyuzhane { get; set; }
        public DbSet<CompanyInfo> PhoneBook { get; set; }
        public DbSet<Law> Laws { get; set; }

        //Вспомогательные
        public DbSet<AlbumTheme> AlbumThemes { get; set; }
        public DbSet<BadWord> BadWords { get; set; }
        public DbSet<CompanyLogo> CompanyLogos { get; set; }

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
            modelBuilder.Entity<Afisha>().Property(a => a.CreatedDate).HasDefaultValueSql("now()");
          
            //Album
            modelBuilder.Entity<Album>().HasIndex(a => a.Name).IsUnique();
            modelBuilder.Entity<Album>().HasIndex(a => a.ThemeId);
            modelBuilder.Entity<Album>().Property(a => a.CreatedDate).HasDefaultValueSql("now()");

            //AlbumPhoto
            modelBuilder.Entity<Album>().Property(a => a.CreatedDate).HasDefaultValueSql("now()");

            //File
            modelBuilder.Entity<File>().HasKey(f => new {f.Id});
            modelBuilder.Entity<File>().Property(f => f.CreatedDate).HasDefaultValueSql("now()");

            //News
            modelBuilder.Entity<News>().Property(f => f.CreatedDate).HasDefaultValueSql("now()");
            modelBuilder.Entity<News>().HasIndex(n => n.NewsType);

            //Law
            modelBuilder.Entity<Law>().Property(l=> l.CreatedDate).HasDefaultValueSql("now()");

            //CommentHistory
            modelBuilder.Entity<CommentHistory>().Property(c => c.CreatedDate).HasDefaultValueSql("now()");

            //HistoryArticle
            modelBuilder.Entity<HistoryArticle>().Property(ha => ha.CreatedDate).HasDefaultValueSql("now()");

            //Organization
            modelBuilder.Entity<Organization>().HasIndex(o => o.OrganizationType);
            modelBuilder.Entity<CompanyLogo>().HasOne(c => c.Organization).WithOne(o => o.CompanyLogo)
                .HasForeignKey<Organization>(o => o.CompanyLogoId).OnDelete(DeleteBehavior.Cascade);
            //Ads
            modelBuilder.Entity<Advertisement>().HasIndex(ad => ad.CategoryId);
            modelBuilder.Entity<Advertisement>().HasIndex(ad => ad.Status);
            modelBuilder.Entity<Advertisement>().Property(ad => ad.CreatedDate).HasDefaultValueSql("now()");

            //Meta
            modelBuilder.Entity<MetaDataInfo>().HasIndex(m => m.TableName).IsUnique();

            //Ustyuzhane
            modelBuilder.Entity<Ustyuzhane>().Property(u => u.CreatedDate).HasDefaultValueSql("now()");

            //User
            modelBuilder.Entity<CommentHistory>()
                .HasOne(ch => ch.User)
                .WithMany(u => u.Comments);
            modelBuilder.Entity<File>()
                .HasOne(f => f.User)
                .WithMany(u => u.Files);

            //BadWords
            modelBuilder.Entity<BadWord>().HasIndex(bw => bw.Word).IsUnique();
        }




        private string GetConnectionString()
        {
            return configuration.GetConnectionString("ConnectionString");
        }


    }
}
