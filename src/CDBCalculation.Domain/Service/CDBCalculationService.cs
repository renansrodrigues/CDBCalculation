using CDBCalculation.Domain.Interface;
using CDBCalculation.Domain.TaxCalculatorStrategies;
using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Domain.Service;

public class CdbCalculationService : ICdbCalculationService
{
    private readonly ICdbCalculationValidator _cDBCalculationValidator;
    private readonly TaxCalculatorStrategyContext _taxCalculatorStrategy;
    const decimal CDI =(0.9M/ 100);
    const decimal TB = (108M / 100);

    public CdbCalculationService(ICdbCalculationValidator cDBCalculationValidator,
        TaxCalculatorStrategyContext taxCalculatorStrategy)
    {
        _cDBCalculationValidator = cDBCalculationValidator;
        _taxCalculatorStrategy = taxCalculatorStrategy; 
        
    }


    public Task<Result<CdbCalculationResult>> DoCDBCalculation(CdbCalculation cdbCalculation)
    {
        
        var validationResult = _cDBCalculationValidator.Validate(cdbCalculation.RedemptionValue, cdbCalculation.TermMonths);

        if (!validationResult.IsSuccess)
        {
            if (validationResult.Error == null)
            {
                return Task.FromResult(Result<CdbCalculationResult>.Failure("Validation error occurred."));
            }
            return Task.FromResult(Result<CdbCalculationResult>.Failure(validationResult.Error));
        }
            
       
        decimal monthlyRate = CDI * TB;
        decimal current = cdbCalculation.RedemptionValue; 
        decimal grossValue = 0;
        decimal NetValue = 0;

        for (int i = 0; i < cdbCalculation.TermMonths; i++) // GET MONTHLY RATE 
        {
            current *= (1 + monthlyRate);   
        }
        

        var taxResult = _taxCalculatorStrategy.ExecuteStrategy(cdbCalculation.TermMonths, current);


        if (!taxResult.IsSuccess) {

            if (taxResult.Error == null)
            {
                return Task.FromResult(Result<CdbCalculationResult>.Failure("Validation error occurred."));
            }

            return    new Task<Result<CdbCalculationResult>>(() =>
            Result<CdbCalculationResult>.Failure(taxResult.Error));         
        }                           
        
            var tax = taxResult.Value;   
            grossValue =  current;
            NetValue = grossValue - tax;

          grossValue = Math.Round(grossValue, 2);
          NetValue = Math.Round(NetValue, 2); 

        return Task.FromResult(
            Result<CdbCalculationResult>.Success( new CdbCalculationResult(grossValue,NetValue)));                                      
    }

}
