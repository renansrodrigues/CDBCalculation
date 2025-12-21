using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Domain.Interface;

public interface IRequestCdbCalculationService
{

    Task<Result<CdbCalculationResult>> DoCDBCalculation(decimal InitialValue, int termMonths);



}
