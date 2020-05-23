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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MPBackends.Models;

namespace MPBackends
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
            services.AddDbContext<UploadvideoContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<UserContext>(opt =>opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<CommentContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<Museum_InformationContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<maintableContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<CollectionContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ExhibitionContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<EducationContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<NewsContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
