using NSE.Identidade.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Resolve Dependecies 
ResolveDependency.ConfigureServices(builder.Services, builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NSE Identity API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();