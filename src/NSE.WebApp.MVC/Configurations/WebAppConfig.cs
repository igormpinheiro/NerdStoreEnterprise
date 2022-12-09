using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Configurations;

public static class WebAppConfig
{
    public static void AddMvcConfiguration(this IServiceCollection services)
    {
        services.AddControllersWithViews();
    }

    public static void UseMvcConfiguration(this WebApplication app, IWebHostEnvironment env)
    {
        //if (!env.IsDevelopment())
        //{
        //    app.UseExceptionHandler("/erro/500");
        //    app.UseStatusCodePagesWithReExecute("/erro/{0}");
        //    app.UseHsts();
        //}
        //else
        //    app.UseDeveloperExceptionPage();

        app.UseExceptionHandler("/erro/500");
        app.UseStatusCodePagesWithReExecute("/erro/{0}");
        app.UseHsts();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityConfiguration();

        app.UseMiddleware<ExceptionMiddleware>();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}