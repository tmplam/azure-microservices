using Microsoft.Extensions.DependencyInjection;
using ProductsService.BusinessLogic.Mappers;

namespace ProductsService.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);

        return services;
    }
}
