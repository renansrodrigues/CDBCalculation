using CDBCalculation.Domain.Entities;

namespace CDBCalculation.Application.DTOs;

public  class CdbCalculationRequestDto
{
    public decimal InitialValue { get; set; }
    public int TermMonths { get; set; }
}

public static class CdbCalculationRequestDtoExtensions
{
    public static CdbCalculation DtoToDomain(this CdbCalculationRequestDto dto)
    {
        return new CdbCalculation(
            dto.InitialValue,
            dto.TermMonths
        );
    }
}


