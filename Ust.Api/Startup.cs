using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ust.Api.Common.Auth;
using Ust.Api.Common.SignalR;
using Ust.Api.Managers.AdsMng;
using Ust.Api.Managers.AfishaMng;
using Ust.Api.Managers.CommentMng;
using Ust.Api.Managers.FileMng;
using Ust.Api.Managers.GalleryMng;
using Ust.Api.Managers.MetaDataInfoMng;
using Ust.Api.Managers.NewsMng;
using Ust.Api.Managers.OrganizationMng;
using Ust.Api.Models.ModelDbObject;

namespace Ust.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ConnectionString")));
            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+аAбБвВгГдДеЕёЁжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩъЪыЫьЬэЭюЮяЯ";
                options.User.RequireUniqueEmail = false;
            });

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin",
                    opt => opt.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });

            services.AddSignalR();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API",
                Version = "v1"
            }));

            DependencyInstaller(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowOrigin");
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseSignalR(config => config.MapHub<CommentHub>("/commentHub"));

            app.UseMvc();

            

            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
        }

        private void DependencyInstaller(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddScoped<INewsManager, NewsManager>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IMetaDataInfoManager, MetaDataInfoManager>();
            services.AddScoped<ICommentManager, CommentManager>();
            services.AddScoped<IAfishaManager, AfishaManager>();
            services.AddScoped<IAdsManager, AdsManager>();
            services.AddScoped<IGalleryManager, GalleryManager>();
            services.AddScoped<IOrganizationManager, OrganizationManager>();
         
            services.BuildServiceProvider();
        }
    }
}
