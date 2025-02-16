using Dapper;

using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace RapidPay.DataAccess.Repositories;

public class UFERepository : IUFERepository
{
    private const string SelectLast =
    @"SELECT 
        u.rate_timestamp AS `TimeStamp`, 
        u.fee_rate AS `Rate`
    FROM
        ufe_rates u
    ORDER BY u.rate_timestamp DESC
    LIMIT 1;";

    private const string AddNewRate = 
    @"INSERT INTO `ufe_rates`
        (`rate_timestamp`, `fee_rate`)
        VALUES
        (Now(), Rand() * 2 * ?p_lastrate);";

    private readonly IDbConnectionFactory _connectionFactory;

    public UFERepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<UfeRate> GetLastRate()
    {
        using var conn = _connectionFactory.CreateConnection();

        var result = await conn.QueryFirstAsync<UfeRate>(SelectLast);

        if (result == null) 
        {
            // Only happens when no rates exist
            await InsertRate(1f);
            result = await conn.QueryFirstAsync<UfeRate>(SelectLast);
        }
       
        return result;
    }

    public async Task InsertRate(float oldRate)
    {
        using var conn = _connectionFactory.CreateConnection();

        await conn.QueryAsync(AddNewRate, new {
            p_lastrate = oldRate
        });
    }
}