
namespace CDBCalculation.Domain.TaxCalculatorStrategies;

public class Upto6MonthsTaxStrategy : ITaxCalculatorStrategy
{

    public bool IsApplicable(int month)
    {
        return month > 1 && month <= 6;
    }

    public decimal Calculate(decimal IncomeValue)
    {
        return IncomeValue * (22.5M / 100M);

    }
}
