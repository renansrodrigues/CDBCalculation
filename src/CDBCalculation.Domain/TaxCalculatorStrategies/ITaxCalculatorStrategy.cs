namespace CDBCalculation.Domain.TaxCalculatorStrategies
{
    public interface ITaxCalculatorStrategy
    {
     
       decimal Calculate(decimal IncomeValue);

       bool IsApplicable(int month);
    
    }
}
