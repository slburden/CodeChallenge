using System.Data;

using Dapper;

using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace RapidPay.DataAccess.Repositories;

public class CardRepository : ICardRepository
{
    private const string SelectAll = @"SELECT 
    cd.`id`, cd.`number`, cd.`active`, cd.`balance`, cd.`limit`
FROM
    card_details cd;";

    private readonly IDbConnectionFactory _connectionFactory;

    public CardRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<CardDetails>> GetAll()
    {

        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<CardDetails>(SelectAll);
    }
}