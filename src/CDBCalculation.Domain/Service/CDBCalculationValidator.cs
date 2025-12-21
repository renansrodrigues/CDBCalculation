using CDBCalculation.Domain.Interface;
using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Domain.Service
{
    public class CdbCalculationValidator : ICdbCalculationValidator
    {


        public Result<CdbCalculationResult> Validate(decimal value, int termMonths)
        {
            if (value <= 0)
                return Result<CdbCalculationResult>.Failure("O valor deve ser positivo");

            if (termMonths <= 1)
                return Result<CdbCalculationResult>.Failure("O prazo deve ser maior que 1 mês");


            return Result<CdbCalculationResult>.SuccessValidation();
        }




    }
}
