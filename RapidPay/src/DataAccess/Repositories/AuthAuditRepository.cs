
using Dapper;

using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace RapidPay.DataAccess;

public class AuthAuditRepository : IAuthAuditRepository
{
    private const string InsertCommand = 
    @"INSERT INTO `audit_authorization`
        (`card_number`,
        `auth_time`,
        `auth_amount`,
        `auth_approved`,
        `auth_denial_reason`)
      VALUES 
        (?p_card_number, NOw(), ?p_auth_amount, ?p_auth_approved, ?p_auth_denial_reason );";

    private readonly IDbConnectionFactory _connectionFactory;

    public AuthAuditRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task InsertAudit(AuthAuditRecord record)
    {
        using var conn = _connectionFactory.CreateConnection();

        await conn.QueryAsync(InsertCommand, new {
            p_card_number = record.CardNumber,
            p_auth_amount = record.Amount,
            p_auth_approved = record.Approved,
            p_auth_denial_reason = record.DenialReason
        });
    }
}
