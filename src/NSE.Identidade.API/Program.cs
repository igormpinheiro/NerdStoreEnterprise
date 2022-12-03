using NSE.Identidade.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();


var app = builder.Build();

app.UseApiConfiguration(builder.Environment);

app.UseSwaggerConfiguration(builder.Environment);

app.Run();