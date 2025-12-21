using CDBCalculation.Domain.Interface;
using CDBCalculation.Domain.TaxCalculatorStrategies;
using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Domain.Service;

public class CDBCalculationService : IRequestCDBCalculationService
{
    private readonly ICDBCalculationValidator _cDBCalculationValidator;
    private readonly TaxCalculatorStrategyContext _taxCalculatorStrategy;
    const decimal CDI =(0.9M/ 100);
    const decimal TB = (108M / 100);

    public CDBCalculationService(ICDBCalculationValidator cDBCalculationValidator,
        TaxCalculatorStrategyContext taxCalculatorStrategy)
    {
        _cDBCalculationValidator = cDBCalculationValidator;
        _taxCalculatorStrategy = taxCalculatorStrategy; 
        
    }


    public Task<Result<CDBCalculationResult>> DoCDBCalculation(decimal InitialValue, int termMonths)
    {
        
        var validationResult = _cDBCalculationValidator.Validate(InitialValue, termMonths);

        if (!validationResult.IsSuccess)
            return Task.FromResult(
                Result<CDBCalculationResult>.Failure(validationResult.Error));

        decimal monthlyRate = CDI * TB;
        decimal current = InitialValue; 
        decimal grossValue = 0;
        decimal NetValue = 0;

        for (int i = 0; i < termMonths; i++) // GET MONTHLY RATE 
        {
            current *= (1 + monthlyRate);   
        }
        

        var taxResult = _taxCalculatorStrategy.ExecuteStrategy(termMonths);


        if (!taxResult.IsSuccess) { 
        
         return    new Task<Result<CDBCalculationResult>>(() =>
            Result<CDBCalculationResult>.Failure(taxResult.Error)); 
        
        }                           
        
            var tax = taxResult.Value;   
            grossValue = current;
            NetValue = grossValue - tax;
                          

        return Task.FromResult(
            Result<CDBCalculationResult>.Success( new CDBCalculationResult(grossValue,NetValue)));                                      
    }

}
