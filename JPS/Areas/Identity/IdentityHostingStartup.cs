using System;
using JPS.Areas.Identity.Data;
using JPS.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(JPS.Areas.Identity.IdentityHostingStartup))]
namespace JPS.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<JPSDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("JPSDbContextConnection")));

                //services.AddDefaultIdentity<JPSUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<JPSDbContext>();
                services.AddIdentity<JPSUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<JPSDbContext>()
                    .AddDefaultUI()
                 .AddDefaultTokenProviders();
            });
        }
    }
}