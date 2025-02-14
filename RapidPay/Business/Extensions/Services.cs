using Microsoft.Extensions.DependencyInjection;

using RapidPay.DataAccess.Extensions;

namespace RapidPay.Business.Extensions;

public static class Services
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
    {
        services.RegisterDataAccessServices();


        return services;
    }
}
