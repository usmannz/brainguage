using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SampleProject.Common;
using SampleProject.Common.Contracts;
using SampleProject.Repository;
using SampleProject.Repository.Contracts;
using SampleProject.Service;
using SampleProject.Service.Contracts;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SampleProject.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            Configuration = builder.Build();


        }

        public IConfiguration Configuration { get; }

        private IServiceCollection Services;

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyCorsPolicy", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });
            this.Services = services;
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var constr = Configuration.GetConnectionString("Default");

            //services.AddDbContext<DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")), ServiceLifetime.Transient);
            //services.AddDbContext<DBContext>(options => options.UseMySql(constr, ServerVersion.AutoDetect(constr)), ServiceLifetime.Transient);
            services.AddDbContext<DBContext>(options => options.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString("Default")));

            services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // configure
            // wt authentication
            var jwtSecret = Encoding.ASCII.GetBytes(Configuration["AppSettings:JwtSigningKey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSecret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddMemoryCache();

            this.ResolveDependencies(services);

            //services.AddHangfire(config =>
            //{
            //    config.UseStorage(new MySqlStorage(AppSettings.ConnectionStringHangfire,
            //    new Hangfire.MySql.MySqlStorageOptions                {
            //        TransactionTimeout = TimeSpan.FromMinutes(5),
            //        QueuePollInterval = TimeSpan.FromSeconds(15),
            //        PrepareSchemaIfNecessary = true,
            //        DashboardJobListLimit = 50000
            //    }));
            //    config.UseSerializerSettings(new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory, IHttpContextAccessor httpContext)
        {


            app.UseStaticFiles();

            //app.UseHangfireDashboard("/api/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new HangfireAuthFilter() }
            //});

            //app.UseHangfireServer();
            app.UseHttpsRedirection();

            InitStaticClasses(env, httpContext);
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}
            app.UseRouting();
            app.UseCors("AllowAnyCorsPolicy");

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "api", pattern: "api/{controller=*}/{action=*}/{id?}");
                endpoints.MapControllerRoute(name: "default", pattern: "{*url}", defaults: new { controller = "Home", action = "Index" });
            });

            loggerFactory.AddFile("logs/{Date}.txt");
            LoadApplicationCache();
        }

        /// <summary>
        /// Init static classes with injection
        /// </summary>
        private void InitStaticClasses(IHostEnvironment env, IHttpContextAccessor httpContext)
        {
            AppSettings.Configuration = (IConfigurationRoot)Configuration;

            AppSettings.Environment = env;

            //Storage.Environment = env;
        }

        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <returns></returns>
        private void ResolveDependencies(IServiceCollection services)
        {
            AppSettings.Configuration = (IConfigurationRoot)Configuration;

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<ISerializer, NewtonsoftSerializer>();
            services.TryAddSingleton<ICache, RedisCacheClient>();
            //services.TryAddSingleton<ICache, MemoryCacheClient>();
            //services.TryAddSingleton<IStorage, FileStorage>();
            services.TryAddTransient<CacheService>();
            services.TryAddTransient<CacheHelper>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IMockTestService, MockTestService>();
            services.AddScoped<IMockTestRepository, MockTestRepository>();
            services.AddScoped<IDemoTestService, DemoTestService>();
            services.AddScoped<IDemoTestRepository, DemoTestRepository>();

        }

        /// <summary>
        /// Get app cache
        /// </summary>
        /// <returns></returns>
        private void LoadApplicationCache()
        {
            var provider = Services.BuildServiceProvider();
            var cacheService = provider.GetService<CacheService>();

            //BackgroundJob.Enqueue(() => provider.GetService<CacheService>().LoadApplicationCache());

            //RecurringJob.AddOrUpdate("RegisterNightlyJobs", () => cacheService.RegisterNightlyJobs(), "0 0 * * *"); // 12:00 AM Midnight Daily
            //RecurringJob.AddOrUpdate("RegisterWeeklyJobs", () => cacheService.RegisterWeeklyJobs(), "0 0 * * 0"); // 12:00 AM Every Sunday

        }
    }

}
