using CDBCalculation.Application.DTOs;
using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Application.Interface;

public interface ICdbCalculationAppService
{

    Task<Result<CdbCalculationResponseDto>> DoCDBCalculation(CdbCalculationRequestDto  cdbCalculationRequestDto);


}
