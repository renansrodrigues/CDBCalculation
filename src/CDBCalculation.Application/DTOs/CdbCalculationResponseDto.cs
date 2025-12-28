using CDBCalculation.Domain.ValueObjects;

namespace CDBCalculation.Application.DTOs;

public record CdbCalculationResponseDto(decimal GrossValue, decimal NetWorth);

  
public static class CdbCalculationResponseDtoExtensions
{
    public static CdbCalculationResponseDto ResponseToDto(CdbCalculationResult domain)
    {
        
        return new CdbCalculationResponseDto(domain.GrossValue, domain.NetWorth);
    }
}





