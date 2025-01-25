using Microsoft.Extensions.DependencyInjection;

namespace ProductsService.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        return services;
    }
}
