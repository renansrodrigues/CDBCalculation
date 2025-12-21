namespace CDBCalculation.Domain.TaxCalculatorStrategies;

public class Over24MonthsTaxStrategy : ITaxCalculatorStrategy
{

    public bool IsApplicable(int month)
    {
        return month > 24;  
    }


    public decimal Calculate(decimal IncomeValue)
    {
        return IncomeValue * (15M / 100M);
    }

  

 
}   


