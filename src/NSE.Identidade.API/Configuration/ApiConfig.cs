namespace NSE.Identidade.API.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();

        // services.AddCors(options =>
        // {
        //     options.AddPolicy("Development",
        //         builder => builder.AllowAnyOrigin()
        //             .AllowAnyMethod()
        //             .AllowAnyHeader()
        //             .AllowCredentials());
        //
        //     options.AddPolicy("Production",
        //         builder => builder.WithMethods("GET")
        //             .WithOrigins("http://meusite.io")
        //             .SetIsOriginAllowedToAllowWildcardSubdomains()
        //             .AllowAnyHeader());
        // });

        return services;
    }
    
    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();

        app.UseRouting();

        // app.UseCors("Development");

        app.UseIdentityConfiguration();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}