using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Domain.TaxCalculatorStrategies;

public class TaxCalculatorStrategyContext
{

    private readonly IEnumerable<ITaxCalculatorStrategy> _strategies; //DI Container Inteject all implementations of ITaxCalculatorStrategy
                                                                      // Every call of IEnumerable<ITaxCalculatorStrategy> will return all implementations registered in the DI container
    public TaxCalculatorStrategyContext(IEnumerable<ITaxCalculatorStrategy> strategies)
    {
        _strategies = strategies;
    }

    public Result<decimal> ExecuteStrategy(int months)
    {
        var strategy = _strategies.FirstOrDefault(s => s.IsApplicable(months));

        if (strategy == null)
            return 
             Result<decimal>.Failure("There is no Tax.");
                    
          return Result<decimal>.Success(
            strategy.Calculate(months));
    }



}
