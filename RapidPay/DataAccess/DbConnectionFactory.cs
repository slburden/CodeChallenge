using System.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MySql.Data.MySqlClient;

using RapidPay.DataAccess.Interfaces;

namespace RapidPay.DataAccess;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string? _connectionString;
    private readonly ILogger<DbConnectionFactory> _logger;

    public DbConnectionFactory(IConfiguration config, ILogger<DbConnectionFactory> logger)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
        _logger = logger;
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}