using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.BuildingRepository;
using ShopSecondHand.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopSecondHand
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

            services.AddDbContext<ShopSecondHandContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //services.AddCors();
            //services.AddControllersWithViews()
            //    .AddNewtonsoftJson()
            //    .AddXmlDataContractSerializerFormatters();

            // Add auto mapper
            services.AddAutoMapper(typeof(Program).Assembly);

            services.AddControllers()
               .AddJsonOptions(x =>
           x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopSecondHand", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopSecondHand v1"));
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
