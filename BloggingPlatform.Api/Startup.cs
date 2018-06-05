using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BloggingPlatform.Db.Model;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BloggingPlatform.Api.Managers.Interfaces;
using BloggingPlatform.Api.Managers;
using BloggingPlatform.Dto;

namespace BloggingPlatform.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(new AutoMapperProfile()));
            services.AddMvc();

            services.AddDbContext<BloggingPlatformContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BloggingDatabase")));

            services.AddTransient<IPostManager, PostManager>();
            services.AddTransient<IAuthorManager, AuthorManager>();
            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<ICommentManager, CommentManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
