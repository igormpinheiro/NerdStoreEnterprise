namespace NSE.Identidade.API.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "NerdStore Enterprise Identity API",
                Version = "v1",
                Description = "Esta API faz parte do curso ASP.NET Core Enterprise Applications.",
                Contact = new()
                {
                    Name = "Igor Pinheiro",
                    Url = new("https://github.com/igormpinheiro")
                },
                License = new()
                {
                    Name = "MIT",
                    Url = new("https://opensource.org/licenses/MIT")
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NerdStore Enterprise Identity API v1");
            });
        }

        return app;
    }
}