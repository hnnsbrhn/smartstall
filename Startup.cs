using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using smartstall.Services;
using System.Configuration;

namespace smartstall
{
    public class Startup
    {        
        
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;
        }

        public IWebHostEnvironment HostEnvironment { get; }
        public IConfiguration Configuration { get; }        

        public void ConfigureServices(IServiceCollection services)
        {
            //create config based on hosting environment (development/production)
            var configpath = "";
            if (HostEnvironment.IsDevelopment())
            {
                configpath = "appsettings.Development.json";
            }
            else
            {
                configpath = "appsettings.Production.json";
            }
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(configpath)
            .Build();
            services.AddSingleton(configuration);

            services.AddControllersWithViews();
            services.AddScoped<DbWriteService>();
            services.AddScoped<DbReadService>();

        }        


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }           
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "receiveData",
                    pattern: "{controller=ReceiveData}/{action=ReceiveData}/{id?}");

                endpoints.MapControllerRoute(
                    name: "hauptseite",
                    pattern: "{controller=Home}/{action=Hauptseite}/{id?}");

                endpoints.MapControllerRoute(
                                    name: "about",
                                    pattern: "{controller=Home}/{action=About}/{id?}");

                endpoints.MapControllerRoute(
                                   name: "faq",
                                   pattern: "{controller=Home}/{action=FAQ}/{id?}");
            });
        }
    }
}
