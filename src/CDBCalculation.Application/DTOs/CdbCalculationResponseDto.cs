using CDBCalculation.Domain.ValueObjects;

namespace CDBCalculation.Application.DTOs;

public class CdbCalculationResponseDto
{
    public CdbCalculationResponseDto(decimal grossValue, decimal netWorth)
    {
        GrossValue = grossValue;
        NetWorth = netWorth;
    }
    public decimal GrossValue { get; set; }
    public decimal NetWorth { get; set; }    

}

public static class CdbCalculationResponseDtoExtensions
{
    public static CdbCalculationResponseDto ResponseToDto(CdbCalculationResult domain)
    {
        return new CdbCalculationResponseDto(domain.GrossValue, domain.NetWorth);
    }
}





