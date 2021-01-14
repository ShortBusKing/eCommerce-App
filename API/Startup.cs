using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using API.Helpers;
using API.Middleware;
using API.Extensions;
using Infrastructure.Identity;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private ConnectionMultiplexer _redis;
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<EcommerceContext>(x => x.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlServer(_config.GetConnectionString("IdentityConnection")));


            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                return ConnectionMultiplexer.Connect(ConfigurationOptions.Parse(_config["ConnectionStringsRedis"], true));

                //RedisConfiguration rs = _config.GetSection("Redis").Get<RedisConfiguration>();
                //return (IConnectionMultiplexer)rs;
            });

            //services.AddSingleton<IConnectionMultiplexer>(c => {
            //    var config = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"));
            //    return ConnectionMultiplexer.Connect(config);
            //});

            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_config["ConnectionStringsRedis"]);
            //// RedisService rs = new RedisService(_config);
            //services.Add .AddDbContext<BasketRepository>();
            //services.AddSingleton(redis);
            //BasketRepository basketRepository = new BasketRepository(redis);

            //services.AddEntityFrameworkSqlServer();
            ////RedisConfiguration rs = _config.GetSection("Redis").Get<RedisConfiguration>();
            //services.AddStackExchangeRedisExtensions<>(XmlConfigurationExtensions => XmlConfigurationExtensions )
            ////services.AddSingleton(rs);
            ////services.AddSingleton(serviceProvider =>
            ////    new BasketRepository(serviceProvider.GetRequiredService<IConnectionMultiplexer>())
            ////);


            services.AddAppServices();
            services.AddIdentityServices(_config);
            services.AddSwaggerDoc();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                   policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwaggerDoc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
