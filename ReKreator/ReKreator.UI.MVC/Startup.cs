using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReKreator.DAL;
using AutoMapper;
using ReKreator.Domain;
using ReKreator.Emailing;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Microsoft.AspNetCore.Identity;
using ReKreator.BL.Interfaces;
using ReKreator.BL.Services;
using ReKreator.DAL.Interfaces;
using ReKreator.DAL.Repositories;

namespace ReKreator.UI.MVC
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("privatesettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<EventContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("azure")));

            services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<EventContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(options =>
            {
                options.AllowValidatingTopLevelNodes = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddAutoMapper();

            DependencyResolve(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            env.EnvironmentName = EnvironmentName.Production;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/Error404");
                app.UseExceptionHandler("/Error/Error500");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// Our Ioc container setup.
        /// </summary>
        /// <param name="container"></param>
        public void DependencyResolve(IServiceCollection container)
        {
            container.AddScoped<IUnitOfWork, EventUnitOfWork>();
            container.AddTransient<IRepository<long, User>, UserRepository>();
            container.AddTransient<IRepository<long, EventHolding_User>, EventHolding_UserRepository>();
            container.AddTransient<IRepository<long, EventHolding>, EventHoldingRepository>();
            container.AddTransient<IRepository<long, Event>, EventRepository>();
            container.AddTransient<IUserService, UserService>();
            container.AddTransient<IEventHoldingService, EventHoldingService>();
            container.AddTransient<IEventService, EventService>();
            container.AddSingleton<ISender>(o =>
                new Sender(Configuration.GetSection("SendGrid").GetSection("apiKey").Value,
                    Configuration.GetSection("Gmail").GetSection("EMail").Value));
            container.AddScoped<IUnitOfWork, EventUnitOfWork>();
            container.AddTransient<IEventService, EventService>();
        }
    }
}