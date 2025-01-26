using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductsService.BusinessLogic.Mappers;
using ProductsService.BusinessLogic.ServiceContracts;
using ProductsService.BusinessLogic.Validators;

namespace ProductsService.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);

        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();

        services.AddScoped<IProductsService, Services.ProductsService>();

        return services;
    }
}
