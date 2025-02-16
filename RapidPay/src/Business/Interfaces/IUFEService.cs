using RapidPay.Models;

namespace RapidPay.Business.Interfaces;

public interface IUFEService
{
    Task<UfeRate> GetRate();
}