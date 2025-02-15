using Dapper;
using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace RapidPay.DataAccess.Repositories;

public class UFERepository
{
    private const string SelectLast =
    @"SELECT 
        u.rate_timestamp AS `TimeStamp`, 
        u.fee_rate AS `Rate`, 
        u.fee_amount AS `Amount`
    FROM
        ufe_rates u
    ORDER BY u.rate_timestamp DESC
    LIMIT 1;";

    private readonly IDbConnectionFactory _connectionFactory;

    public UFERepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<UfeRate> GetLastRate() {
        using var conn = _connectionFactory.CreateConnection();

        return await conn.QueryFirstAsync<UfeRate>(SelectLast);
    }
}