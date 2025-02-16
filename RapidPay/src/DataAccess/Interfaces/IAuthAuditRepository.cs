using RapidPay.Models;

namespace RapidPay.DataAccess;

public interface IAuthAuditRepository
{
    Task InsertAudit(AuthAuditRecord record);
}
