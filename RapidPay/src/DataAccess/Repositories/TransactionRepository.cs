using System.Formats.Asn1;

using Dapper;

using Microsoft.Extensions.Logging;

using MySql.Data.MySqlClient;

using RapidPay.DataAccess.Interfaces;

namespace RapidPay.DataAccess.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private const string InsertTransactionQuery =
    @"INSERT INTO `transaction`
        (`card_number`, `amount`, `fee_amount`)
        VALUES
        (?p_cardnumber, ?p_amount, ?p_fee);";


    private const string CheckForDuplicateQuery = 
    @"SELECT 
    EXISTS( SELECT 
            1
        FROM
            `transaction`
        WHERE
            ?p_cardnumber = '' AND 
            charge_time = CURRENT_TIMESTAMP() AND 
            amount = ?p_amount) AS record;";

    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<TransactionRepository> _logger;
    public TransactionRepository(IDbConnectionFactory connectionFactory, ILogger<TransactionRepository> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task InsertTransaction(string cardnumber, decimal amount, decimal fee)
    {
        using var conn = _connectionFactory.CreateConnection();

        try
        {
            await conn.QueryAsync(InsertTransactionQuery, new
            {
                p_cardnumber = cardnumber,
                p_amount = amount,
                p_fee = fee
            });
        }
        catch (MySqlException mx)
        {
            _logger.LogError(mx, "Error inserting Transaction");
            throw new InvalidOperationException("Cannot insert transaction since it would be a duplicate");
        }
    }

    public async Task<bool> TransactionExists(string cardnumber, decimal amount) {

        using var conn = _connectionFactory.CreateConnection();

        return await conn.QueryFirstAsync<bool>(CheckForDuplicateQuery, new {
                p_cardnumber = cardnumber,
                p_amount = amount,
            });
    }
}