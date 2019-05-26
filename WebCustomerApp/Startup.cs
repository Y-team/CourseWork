﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCustomerApp.Data;
using WebCustomerApp.Models;
using WebCustomerApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Model.Interfaces;
using DAL.Repositories;
using BAL.Managers;
using AutoMapper;
using BAL.Interfaces;
using BAL.Services;
using BAL.Jobs;

namespace WebCustomerApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
			services.AddTransient<ITariffRepository, TariffRepository>();
			services.AddTransient<IBaseRepository<Tariff>, BaseRepository<Tariff>>();
			services.AddTransient<IBaseRepository<Company>, BaseRepository<Company>>();
            services.AddTransient<IMailingRepository, MailingRepository>();
            services.AddTransient<IBaseRepository<ApplicationGroup>, BaseRepository<ApplicationGroup>>();

            services.AddTransient<IBaseRepository<Basket>, BaseRepository<Basket>>();
            services.AddTransient<IBaseRepository<BlockedUser>, BaseRepository<BlockedUser>>();
            services.AddTransient<IBaseRepository<Commodity>, BaseRepository<Commodity>>();
            services.AddTransient<IBaseRepository<LongDescription>, BaseRepository<LongDescription>>();
            services.AddTransient<IBaseRepository<Moderator>, BaseRepository<Moderator>>();
            services.AddTransient<IBaseRepository<OrderUser>, BaseRepository<OrderUser>>();
            services.AddTransient<IBaseRepository<Photo>, BaseRepository<Photo>>();
            services.AddTransient<IBaseRepository<BasketCommodities>, BaseRepository<BasketCommodities>>();



            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");});
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");});
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings  
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings  
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings  
                options.User.RequireUniqueEmail = true;
            });


            //Seting the Account Login page  
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings  
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/NewLogin"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login  
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout  
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied  
                options.SlidingExpiration = true;
            });
            services.AddMvc();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICompanyManager, CompanyManager>();
            services.AddScoped<IRecipientManager, RecipientManager>();
            services.AddScoped<IContactManager, ContactManager>();
            services.AddScoped<ITariffManager, TariffManager>();
            services.AddScoped<IPhoneManager, PhoneManager>();
            services.AddScoped<IStopWordManager, StopWordManager>();
            services.AddScoped<IGroupManager, GroupManager>();
            services.AddScoped<IOperatorManager, OperatorManager>();
            services.AddScoped<ICodeManager, CodeManager>();
            services.AddScoped<IMailingManager, MailingManager>();

            services.AddScoped<IBlockedUserManager, BlockedUserManager>();
            services.AddScoped<ICommodityManager, CommodityManager>();
            services.AddScoped<ILongDescriptionManager, LongDescriptionManager>();
            services.AddScoped<IModeratorManager, ModeratorManager>();
            services.AddScoped<IOrderUserManager, OrderUserManager>();
            services.AddScoped<IPhotoManager, PhotoManager>();
            services.AddScoped<IBasketManager, BasketManager>();
            services.AddScoped<IBasketCommoditiesManager, BasketCommoditiesManager>();
            // Start scheduler

            services.AddScoped<Mailing>();
            MailingScheduler.Start(services.BuildServiceProvider());

            // Configure sessions

            services.AddDistributedMemoryCache();
            services.AddSession();
        }
       
        public static class MyIdentityDataInitializer
        {
            public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                SeedRoles(roleManager);
                SeedUsers(userManager);
            }
           
            
            public static void SeedUsers(UserManager<ApplicationUser> userManager)
            {
                if (userManager.FindByNameAsync("User@gmail.com").Result == null)
                {
                    ApplicationUser user = new ApplicationUser();
                    ApplicationGroup group = new ApplicationGroup();
                    user.UserName = "User@gmail.com";
                    user.Email = "User@gmail.com";
                    user.ApplicationGroup = group;
                   
                   IdentityResult result = userManager.CreateAsync(user,"1234ABCD").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user,"User").Wait();
                    }
                }


                if (userManager.FindByNameAsync("Admin@gmail.com").Result == null)
                {
                    ApplicationUser user = new ApplicationUser();
                    ApplicationGroup group = new ApplicationGroup();
                    user.UserName = "Admin@gmail.com";
                    user.Email = "Admin@gmail.com";
                    user.ApplicationGroup = group;

                    IdentityResult result;
                        result = userManager.CreateAsync(user,"1234ABCD").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }

                if (userManager.FindByNameAsync("Moderator@gmail.com").Result == null)
                {
                    ApplicationUser user = new ApplicationUser();
                    ApplicationGroup group = new ApplicationGroup();
                    user.UserName = "Moderator@gmail.com";
                    user.Email = "Moderator@gmail.com";
                    user.ApplicationGroup = group;

                    IdentityResult result;
                    result = userManager.CreateAsync(user, "1234ABCD").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Moderator").Wait();
                    }
                }
            }

            public static void SeedRoles(RoleManager<IdentityRole> roleManager)
            {
                if (!roleManager.RoleExistsAsync("User").Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = "User";
                    IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
                }


                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = "Admin";
                    IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
                }

                if (!roleManager.RoleExistsAsync("Moderator").Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = "Moderator";
                    IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
                }
            }
        }

        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            // Configure sessions

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");    
            });

        }
    }
}
