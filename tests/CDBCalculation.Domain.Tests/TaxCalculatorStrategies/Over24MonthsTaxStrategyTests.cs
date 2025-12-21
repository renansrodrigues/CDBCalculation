using CDBCalculation.Domain.TaxCalculatorStrategies;

namespace CDBCalculation.Domain.Tests.TaxCalculatorStrategies;

public class Over24MonthsTaxStrategyTests
{

    private readonly Over24MonthsTaxStrategy _strategy;

    public Over24MonthsTaxStrategyTests()
    {
        _strategy = new Over24MonthsTaxStrategy();
    }


    [Theory]
    [InlineData(37)]
    [InlineData(25)]
    public void IsApplicable_ShouldReturnTrue_WhenMonthsOver24(int months)
    {
        // Act
        var result = _strategy.IsApplicable(months);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(24)]
    public void IsApplicable_ShouldReturnFalse_WhenMonthsIsOutOfRange(int months)
    {
        // Act
        var result = _strategy.IsApplicable(months);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Calculate_ShouldReturn150PercentOfGrossIncome()
    {
        // Arrange
        decimal grossIncome = 1000m;

        // Act
        var tax = _strategy.Calculate(grossIncome);

        // Assert
        Assert.Equal(150m, tax);
    }


}
