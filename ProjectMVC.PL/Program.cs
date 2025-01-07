
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectMVC.DAL.Data;
using ProjectMVC.DAL.Models;
using ProjectMVC.PL.Extensions;
using ProjectMVC.PL.Helpers;
using ProjectMVC.PL.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);

            #region ConfigureServices
            Builder.Services.AddControllersWithViews();

            //services.AddSingleton<AppDbContext>(); // Life time --> Per Application
            //services.AddScoped<AppDbContext>(); // Life time --> Per Request
            //services.AddTransient<AppDbContext>(); // Life Time --> Per Operation

            Builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // Default --> Scoped

            Builder.Services.AddApplictionServices(); // Extension Method
            Builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityRole>>();
            Builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config => {
                //config.Password.RequiredUniqueChars = 2;
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = true;
                config.Password.RequireNonAlphanumeric = false;

                //config.User.RequireUniqueEmail = true;

                //config.Lockout.MaxFailedAccessAttempts = 5;
                //config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            Builder.Services.Configure<Services.Settings.MailSettings>(Builder.Configuration.GetSection("MailSettings"));

            Builder.Services.AddTransient<IMailSettings, Helpers.MailSettings>();
            #endregion

            var app = Builder.Build();

            #region Configure
            if (Builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion

            app.Run();
            //CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
