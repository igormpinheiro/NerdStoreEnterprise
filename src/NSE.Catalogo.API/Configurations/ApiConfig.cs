using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Data;

namespace NSE.Catalogo.API.Configurations;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        //services.AddResponseCompression();

        services.AddSwaggerConfiguration();

        return services;
    }

    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRouting();

        app.UseCors("Total");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwaggerConfiguration();

        return app;
    }
}