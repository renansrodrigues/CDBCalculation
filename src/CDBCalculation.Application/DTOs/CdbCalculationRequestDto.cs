using CDBCalculation.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CDBCalculation.Application.DTOs;

public  class CdbCalculationRequestDto
{
    [Required]
    public decimal InitialValue { get; set; }
    [Required]
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



