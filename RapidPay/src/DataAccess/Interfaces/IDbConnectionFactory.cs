using System.Data;

namespace RapidPay.DataAccess.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}