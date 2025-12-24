using CDBCalculation.Domain.Entities;
using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Domain.Interface;

public interface ICdbCalculationService
{

    Task<Result<CdbCalculationResult>> DoCDBCalculation(CdbCalculation cdbCalculation);



}
