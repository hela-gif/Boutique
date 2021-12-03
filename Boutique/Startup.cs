
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Boutique.Data;
using Boutique.Models;
using Microsoft.AspNetCore.Identity;

namespace Boutique
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
            //DbContext Configuration 
            //services.AddDbContext<BoutiqueDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BoutiqueContext")));
            //Servives Configuration
            //services.AddScoped<IProductsService, ProductsService>();
            services.AddSession();
            services.AddControllersWithViews();

            services.AddDbContext<BoutiqueContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BoutiqueContext")));
            services.AddIdentity<Personne, IdentityRole>().AddEntityFrameworkStores<BoutiqueContext>();
            //services.AddIdentity<Client, IdentityRole>().AddEntityFrameworkStores<BoutiqueContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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
            app.UseSession();
           

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
