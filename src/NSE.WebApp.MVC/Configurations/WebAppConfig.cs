using Microsoft.AspNetCore.Localization;
using NSE.WebApp.MVC.Extensions;
using System.Globalization;

namespace NSE.WebApp.MVC.Configurations;

public static class WebAppConfig
{
    public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();

        services.Configure<AppSettings>(configuration);
    }

    public static void UseMvcConfiguration(this WebApplication app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithReExecute("/erro/{0}");
            app.UseHsts();
        }
        else
            app.UseDeveloperExceptionPage();

        //app.UseExceptionHandler("/erro/500");
        //app.UseStatusCodePagesWithReExecute("/erro/{0}");
        app.UseHsts();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityConfiguration();

        var supportedCulture = new[] { new CultureInfo("pt-BR") };
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = supportedCulture,
            SupportedUICultures = supportedCulture
        });

        app.UseMiddleware<ExceptionMiddleware>();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Catalog}/{action=Index}/{id?}");
    }
}