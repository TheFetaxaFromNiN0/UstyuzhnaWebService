using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ust.Api.Common.Auth;
using Ust.Api.Common.Selenium;
using Ust.Api.Managers.FileMng;
using Ust.Api.Managers.MetaDataInfoMng;
using Ust.Api.Managers.NewsMng;
using Ust.Api.Models;
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

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin",
                    opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });

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

            app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowCredentials().AllowAnyMethod());
            app.UseAuthentication();
            app.UseHttpsRedirection();
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
            services.AddScoped<ISeleniumWorker, SeleniumWorker>();
         
            services.BuildServiceProvider();
        }
    }
}
