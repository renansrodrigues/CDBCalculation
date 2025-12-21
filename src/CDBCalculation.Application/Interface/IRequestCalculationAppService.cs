using CDBCalculation.Application.DTOs;

namespace CDBCalculation.Application.Interface;

public interface IRequestCalculationAppService
{

    Task<CDBCalculationResultDto> DoCDBCalculation(decimal value, int termMonths);


}
