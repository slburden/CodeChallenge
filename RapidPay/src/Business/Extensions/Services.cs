using Microsoft.Extensions.DependencyInjection;

using RapidPay.Business.Interfaces;
using RapidPay.Business.Services;
using RapidPay.DataAccess.Extensions;

namespace RapidPay.Business.Extensions;

public static class Services
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
    {
        services.RegisterDataAccessServices();

        services.AddScoped<IPaymentAuthService, PaymentAuthService>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IUFEService, UFEService>();

        return services;
    }
}