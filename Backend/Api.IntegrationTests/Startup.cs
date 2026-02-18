using Api.Config;
using Api.Controllers;
using Api.Persistence.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTests
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddApplicationPart(typeof(ApiAutenticadoController).Assembly);

            var connectionString = Configuration["ConnectionStrings:Default"];
            var useSqlite = connectionString.Contains(".db") || connectionString.Contains("Data Source=") && !connectionString.Contains("LocalDb");
            if (useSqlite)
            {
                services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
            }
            else
            {
                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            }

            services.ConfigurarAppSettingsComoObjetoTipado(Configuration);

            services.ConfigurarAutenticacionJWT(Configuration);

            services.ConfigurarInyeccionDeDependecias();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {
            var connectionString = Configuration["ConnectionStrings:Default"];
            var useSqlite = connectionString.Contains(".db") || (connectionString.Contains("Data Source=") && !connectionString.Contains("LocalDb"));
            if (useSqlite)
            {
                dbContext.Database.EnsureCreated();
            }
            else
            {
                dbContext.Database.Migrate();
            }
            
            app.ConfigurarExceptionMiddleware();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
