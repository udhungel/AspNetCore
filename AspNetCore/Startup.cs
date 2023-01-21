using EmployeeManagement_AspNetCore.Interface;
using EmployeeManagement_AspNetCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore
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
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeSQLConnection")));

            //Identityuser => It has properties like Email,TwoFactorAuthentication , Entity frameword core is used to get user 
            services.AddIdentity<IdentityUser,IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();            
            
            /////Old Way
            //services.AddMvc(options=>options.EnableEndpointRouting = false);

        
            // New Ways - it call addmvc method 
            services.AddRazorPages();

            // we want the instance of the sql server instance to be alive and available throught out a single HTTP request
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

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
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
               
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            

            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthorization();

            // this needs to added before the UseMvc routes 
            app.UseAuthentication(); 

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});

            // app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints =>
            {
                // need to pass the MapRoute and default convention 
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
