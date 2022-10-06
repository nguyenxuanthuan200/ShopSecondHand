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
using ShopSecondHand.Repository.CategoryRepository;
using ShopSecondHand.Repository.OrderDetailRepository;
using ShopSecondHand.Repository.OrderRepository;
using ShopSecondHand.Repository.ProductRepository;
using ShopSecondHand.Repository.AccountRepository;
using ShopSecondHand.Service;
using ShopSecondHand.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VoiceAPI.Configure;

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

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();


            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();


            // Cors
            services.AddCors(opt => opt.AddDefaultPolicy(builder => builder.AllowAnyOrigin()
                                                                           .AllowAnyMethod()
                                                                           .AllowAnyHeader()));
            //services.AddCors();
            //services.AddControllersWithViews()
            //    .AddNewtonsoftJson()
            //    .AddXmlDataContractSerializerFormatters();

            // Compression
            services.AddResponseCompression();
            // Add auto mapper
            //services.AddAutoMapper(typeof(Program).Assembly);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Config
            services.Configure<JwtConfig>(Configuration.GetSection("Jwt"));
            // Disable ModelStateInvalidFilter
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


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
