using ProductsService.DataAccess;
using ProductsService.BusinessLogic;
using FluentValidation.AspNetCore;
using ProductsService.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDataAccess(builder.Configuration)
    .AddBusinessLogic();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();


var app = builder.Build();


app.UseExceptionHandlingMiddleware();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
