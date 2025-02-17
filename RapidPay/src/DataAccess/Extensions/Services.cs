using Microsoft.Extensions.DependencyInjection;

using RapidPay.DataAccess.Interfaces;
using RapidPay.DataAccess.Repositories;

namespace RapidPay.DataAccess.Extensions;

public static class Services
{
    public static IServiceCollection RegisterDataAccessServices(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IAuthAuditRepository, AuthAuditRepository>();
        services.AddScoped<IUFERepository, UFERepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}