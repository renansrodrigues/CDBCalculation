using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Domain.Interface;

public interface IRequestCDBCalculationService
{

    Task<Result<CDBCalculationResult>> DoCDBCalculation(decimal value, int termMonths);



}
