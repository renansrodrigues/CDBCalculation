using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Domain.Interface;

public interface ICdbCalculationValidator
{

    Result<CdbCalculationResult> Validate(decimal value, int termMonths);

}
