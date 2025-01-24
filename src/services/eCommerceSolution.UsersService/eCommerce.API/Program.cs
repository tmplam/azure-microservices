using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddInfrastructure();

builder.Services.AddControllers();


var app = builder.Build();


app.UseExceptionHandlingMiddleware();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
