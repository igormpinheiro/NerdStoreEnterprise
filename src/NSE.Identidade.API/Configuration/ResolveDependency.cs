using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSE.Identidade.API.Data;

namespace NSE.Identidade.API.Configuration;

public class ResolveDependency
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        ConfigureDatabase(services, configuration);
        ConfigureIdentity(services);
        // // Infra - Data
        // services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        // services.AddScoped<IUnitOfWork, UnitOfWork>();
        // services.AddScoped<IdentidadeContext>();
        //
        // // Infra - Identity
        // services.AddScoped<IAspNetUser, AspNetUser>();
        //
        // // Services
        // services.AddScoped<IUsuarioService, UsuarioService>();
    }

    private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }

    private static void ConfigureIdentity(IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
    }
}