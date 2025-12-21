using CDBCalculation.Application.DTOs;
using CDBCalculation.Application.Interface;

namespace CDBCalculation.Application.AppServices;

public class RequestCalculationAppService : IRequestCalculationAppService
{
    public Task<CDBCalculationResultDto> DoCDBCalculation(decimal value, int termMonths)
    {
        throw new NotImplementedException();
    }
}
