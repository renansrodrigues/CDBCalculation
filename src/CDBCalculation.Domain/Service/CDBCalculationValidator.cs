using CDBCalculation.Domain.Interface;
using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CDBCalculation.Domain.Service
{
    public class CdbCalculationValidator : ICdbCalculationValidator
    {


        public Result<CdbCalculationResult> Validate(decimal value, int termMonths)
        {
            if (value <= 0)
                return Result<CdbCalculationResult>.Failure("The value must be a positive number.");

            if (termMonths <= 1)
                return Result<CdbCalculationResult>.Failure("The TermMonths must be greater than one month.");


            return Result<CdbCalculationResult>.SuccessValidation();
        }




    }
}
