using NSE.Identidade.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var app = builder.Build();

app.UseApiConfiguration(builder.Environment);

app.UseSwaggerConfiguration(builder.Environment);

app.Run();