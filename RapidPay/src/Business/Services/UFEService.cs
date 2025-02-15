using RapidPay.DataAccess.Interfaces;
using RapidPay.Business.Interfaces;

namespace RapidPay.Business.Services;

public class UFEService : IUFEService
{
    private readonly IUFERepository _uFERepository;

    public UFEService(IUFERepository uFERepository)
    {
        _uFERepository = uFERepository;
    }


}