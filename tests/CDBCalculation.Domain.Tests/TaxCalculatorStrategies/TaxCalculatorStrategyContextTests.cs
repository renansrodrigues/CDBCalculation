using CDBCalculation.Domain.TaxCalculatorStrategies;

namespace CDBCalculation.Domain.Tests.TaxCalculatorStrategies;

public class TaxCalculatorStrategyContextTests
{
    private readonly TaxCalculatorStrategyContext  _context;

    public  ITaxCalculatorStrategy[] strategies;
    
          
    public TaxCalculatorStrategyContextTests()
    {

        strategies = new ITaxCalculatorStrategy[]
       {
            new Upto6MonthsTaxStrategy(),
            new Upto12MonthsTaxStrategy(),
            new Upto24MonthsTaxStrategy(),
            new Over24MonthsTaxStrategy()   
       };
      _context = new TaxCalculatorStrategyContext(strategies);

    }

    [Fact]
    public void TaxCalculatorStrategyContext_shouldReturnDecimal_WhenMonthsIs1And6()
    {
        // Arrange
        var fakerRange = new Bogus.Faker();
        int fakermonths = fakerRange.Random.Int(1, 6);
        decimal fakeIncomeValue = fakerRange.Finance.Amount(1000, 10000);   

        // Act
        var strategy = _context.ExecuteStrategy(fakermonths, fakeIncomeValue);

        // Assert        
        Assert.IsType<decimal>(strategy.Value);
    }

    [Fact]
    public void TaxCalculatorStrategyContext_shouldReturnDecimal_WhenMonthsIsBetween7And12()
    {
        // Arrange
        var fakerRange = new Bogus.Faker();
        int fakermonths = fakerRange.Random.Int(7, 12);
        decimal fakeIncomeValue = fakerRange.Finance.Amount(1000, 10000);
        // Act
        var strategy = _context.ExecuteStrategy(fakermonths, fakeIncomeValue);
        // Assert
        Assert.IsType<decimal>(strategy.Value);
    }
    [Fact]
    public void TaxCalculatorStrategyContext_shouldReturnDecimal_WhenMonthsIsBetween13And24()
    {
        // Arrange
        var fakerRange = new Bogus.Faker();
        int fakermonths = fakerRange.Random.Int(13, 24);
        decimal fakeIncomeValue = fakerRange.Finance.Amount(1000, 10000);
        // Act
        var strategy = _context.ExecuteStrategy(fakermonths, fakeIncomeValue);

        // Assert
        Assert.IsType<decimal>(strategy.Value);
    }

    [Fact]
    public void TaxCalculatorStrategyContext_shouldReturnDecimal_WhenMonthsIsOver24()
    {
        // Arrange
        var fakerRange = new Bogus.Faker();
        int fakermonths = fakerRange.Random.Int(25, 100);
        decimal fakeIncomeValue = fakerRange.Finance.Amount(1000, 10000);
        // Act
        var strategy = _context.ExecuteStrategy(fakermonths, fakeIncomeValue);

        // Assert
        Assert.IsType<decimal>(strategy.Value);
    }






}
