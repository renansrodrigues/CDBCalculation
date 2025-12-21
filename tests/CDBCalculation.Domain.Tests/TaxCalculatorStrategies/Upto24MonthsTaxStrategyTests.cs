using CDBCalculation.Domain.TaxCalculatorStrategies;

namespace CDBCalculation.Domain.Tests.TaxCalculatorStrategies;

public class Upto24MonthsTaxStrategyTests
{
    private readonly Upto24MonthsTaxStrategy _strategy;

    public Upto24MonthsTaxStrategyTests()
    {
        _strategy = new Upto24MonthsTaxStrategy();
    }


    [Theory]
    [InlineData(13)]
    [InlineData(24)]
    public void IsApplicable_ShouldReturnTrue_WhenMonthsBetween13And24(int months)
    {
        // Act
        var result = _strategy.IsApplicable(months);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(25)]
    [InlineData(30)]
    public void IsApplicable_ShouldReturnFalse_WhenMonthsIsOutOfRange(int months)
    {
        // Act
        var result = _strategy.IsApplicable(months);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Calculate_ShouldReturn200PercentOfGrossIncome()
    {
        // Arrange
        decimal grossIncome = 1000m;

        // Act
        var tax = _strategy.Calculate(grossIncome);

        // Assert
        Assert.Equal(175m, tax);
    }


}
