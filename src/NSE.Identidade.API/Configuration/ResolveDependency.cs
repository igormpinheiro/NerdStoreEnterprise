using Microsoft.EntityFrameworkCore;
using NSE.Identidade.API.Data;

namespace NSE.Identidade.API.Configuration;

public class ResolveDependency
{
    
    //Configure DBContext
    public static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }
    
    public static void ConfigureServices(IServiceCollection services)
    {
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
    
}