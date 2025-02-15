using RapidPay.Business.Extensions;

namespace RapidPay.Api.Extensions;

public static class Services
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services)
    {
        services.RegisterBusinessServices();


        return services;
    }
}