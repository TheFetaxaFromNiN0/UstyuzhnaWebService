using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ust.Api.BizRules.User;

namespace Ust.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IUserBizRules UserBizRules { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            services.AddSingleton<IUserBizRules,UserBizRules>();

            services.BuildServiceProvider();
        }
    }
}
