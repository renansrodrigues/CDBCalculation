using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Domain.Interface;

public interface ICDBCalculationValidator
{

    Result<CDBCalculationResult> Validate(decimal value, int termMonths);

}
