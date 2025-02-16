using RapidPay.Business.Interfaces;
using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace RapidPay.Business.Services;

public class UFEService : IUFEService
{
    private readonly IUFERepository _uFERepository;

    public UFEService(IUFERepository uFERepository)
    {
        _uFERepository = uFERepository;
    }

    public async Task<UfeRate> GetRate()
    {
        var rate = await _uFERepository.GetLastRate();

        if (rate.TimeStamp < DateTime.Now.AddHours(-1)){

            await _uFERepository.InsertRate(rate.Rate);
            rate = await _uFERepository.GetLastRate();

        }

        return rate;
    }
}