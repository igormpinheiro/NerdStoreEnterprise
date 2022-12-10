using Microsoft.OpenApi.Models;

namespace NSE.Catalogo.API.Configurations;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "NerdStore Enterprise Catálogo API",
                Description = "Esta API faz parte do curso ASP.NET Core Enterprise Applications.",
                Contact = new OpenApiContact()
                {
                    Name = "Igor Pinheiro",
                    Email = "igordf@gmail.com"
                },
                License = new OpenApiLicense()
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
        });
        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NSE.Catalogo.API v1"));

        return app;
    }
}
