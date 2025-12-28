using CDBCalculation.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace CDBCalculation.Application.DTOs;

public  class CdbCalculationRequestDto
{
    [Range(0.01, double.MaxValue)]
    public decimal RedemptionValue { get; set; }
    
    [Range(2, int.MaxValue)]
    public int TermMonths { get; set; }
}

public static class CdbCalculationRequestDtoExtensions
{
    public static CdbCalculation DtoToDomain(this CdbCalculationRequestDto dto)
    {
        return new CdbCalculation(
            dto.RedemptionValue,
            dto.TermMonths
        );
    }
}






