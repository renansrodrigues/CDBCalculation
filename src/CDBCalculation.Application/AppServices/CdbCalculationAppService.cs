using CDBCalculation.Application.DTOs;
using CDBCalculation.Application.Interface;
using CDBCalculation.Domain.Interface;
using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Application.AppServices;

public class CdbCalculationAppService : ICdbCalculationAppService
{
    private readonly ICdbCalculationService _cdbCalculationService;

    public CdbCalculationAppService(ICdbCalculationService cdbCalculationService)
    {
        _cdbCalculationService = cdbCalculationService;
    }

    public async Task<Result<CdbCalculationResponseDto>> DoCDBCalculation(CdbCalculationRequestDto cdbCalculationRequestDto)
    {
        try
        {
            var result = await
            _cdbCalculationService.
            DoCDBCalculation(CdbCalculationRequestDtoExtensions.
            DtoToDomain(cdbCalculationRequestDto));


            if (!result.IsSuccess)
            {
                return Result<CdbCalculationResponseDto>.Failure(result.Error);
            }
            if (result is null || result.Value is null)
            {
                return Result<CdbCalculationResponseDto>.Failure("Error ,pleasy try again later.");
            }

            var responseDto = CdbCalculationResponseDtoExtensions.ResponseToDto(result.Value);
            var response = Result<CdbCalculationResponseDto>.Success(responseDto);

            return response;
        }
        catch (Exception ex)
        {
            return Result<CdbCalculationResponseDto>.Failure(ex.Message);
        }
      
    }
}
