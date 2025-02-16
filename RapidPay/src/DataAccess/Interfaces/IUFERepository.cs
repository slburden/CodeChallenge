using RapidPay.Models;
namespace RapidPay.DataAccess.Interfaces;

public interface IUFERepository
{
    Task<UfeRate> GetLastRate();

    Task InsertRate(float oldRate);
}