using RapidPay.Business.Interfaces;
using RapidPay.DataAccess.Interfaces;

namespace RapidPay.Business.Services;

public class UFEService : IUFEService
{
    private readonly IUFERepository _uFERepository;

    public UFEService(IUFERepository uFERepository)
    {
        _uFERepository = uFERepository;
    }


}