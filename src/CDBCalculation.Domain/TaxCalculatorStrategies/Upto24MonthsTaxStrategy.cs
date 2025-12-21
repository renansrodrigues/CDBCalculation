namespace CDBCalculation.Domain.TaxCalculatorStrategies;


public class Upto24MonthsTaxStrategy : ITaxCalculatorStrategy
{

    public bool IsApplicable(int month)
    {
        return  month > 12 && month <=24;
    }


    public decimal Calculate(decimal IncomeValue)
    {
        return IncomeValue * (17.5M / 100M);

    }
}   


