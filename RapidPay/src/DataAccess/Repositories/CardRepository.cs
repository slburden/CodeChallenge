using Dapper;

using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace RapidPay.DataAccess.Repositories;

public class CardRepository : ICardRepository
{
    private const string SELECTAll =
    @"SELECT 
        cd.`id`, 
        cd.`number`, 
        cd.`active`, 
        cd.`balance`, 
        cd.`limit`
    FROM
        card_details cd;";

    private const string GetByNumber =
    @"SELECT 
        cd.`id`, 
        cd.`number`, 
        cd.`active`, 
        cd.`balance`, 
        cd.`limit`
    FROM
        card_details cd
    WHERE 
        cd.`number` = ?p_number;";

    private const string DoesCardExist =
    @"SELECT EXISTS
        (SELECT 1
        FROM card_details cd
        WHERE cd.`number` = ?p_number) as record_exists";

    private const string CardInsert =
    @"INSERT INTO `RapidPay`.`card_details`
        (`number`, `active`, `balance`, `limit`)
    VALUES
        (?p_number, ?p_active, ?p_balance, ?p_limit)
     ON DUPLICATE KEY UPDATE
       `active` = VALUES(`active`),
        `balance` = VALUES(`balance`),
        `limit` = VALUES(`limit`)";

    private readonly IDbConnectionFactory _connectionFactory;

    public CardRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CardExists(string number)
    {
        using var conn = _connectionFactory.CreateConnection();

        return await conn.QuerySingleAsync<bool>(DoesCardExist,
            new
            {
                p_number = number
            });
    }

    public async Task<IEnumerable<CardDetails>> GetAll()
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<CardDetails>(SELECTAll);
    }

    public async Task<CardDetails> GetCardByNumber(string number)
    {
        using var conn = _connectionFactory.CreateConnection();

        return await conn.QueryFirstAsync<CardDetails>(GetByNumber,
            new
            {
                p_number = number
            });
    }

    public async Task UpsertCard(CardDetails details)
    {
        using var conn = _connectionFactory.CreateConnection();

        await conn.QueryAsync(CardInsert,
            new
            {
                p_number = details.Number,
                p_active = details.Active,
                p_balance = details.Balance,
                p_limit = details.Limit
            });
    }
}