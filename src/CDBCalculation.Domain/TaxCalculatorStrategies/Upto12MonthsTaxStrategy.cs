namespace CDBCalculation.Domain.TaxCalculatorStrategies;

public class Upto12MonthsTaxStrategy : ITaxCalculatorStrategy
{
    public bool IsApplicable(int month)
    {
        return month > 6 && month <= 12;
    }


    public decimal Calculate(decimal IncomeValue)
    {
        return IncomeValue * (20M / 100M);

    }
}   

