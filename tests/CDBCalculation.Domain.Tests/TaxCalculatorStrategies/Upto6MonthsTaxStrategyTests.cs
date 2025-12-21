using CDBCalculation.Domain.TaxCalculatorStrategies;

namespace CDBCalculation.Domain.Tests.TaxCalculatorStrategies;
public class Upto6MonthsTaxStrategyTests
{


    private readonly Upto6MonthsTaxStrategy _strategy;

    public Upto6MonthsTaxStrategyTests()
    {
        _strategy = new Upto6MonthsTaxStrategy();
    }


    [Theory]
    [InlineData(1)]
    [InlineData(6)]
    public void IsApplicable_ShouldReturnTrue_WhenMonthsBetween1And6(int months)
    {
        // Act
        var result = _strategy.IsApplicable(months);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(7)]
    [InlineData(15)]
    public void IsApplicable_ShouldReturnFalse_WhenMonthsIsOutOfRange(int months)
    {
        // Act
        var result = _strategy.IsApplicable(months);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Calculate_ShouldReturn225PercentOfGrossIncome()
    {
        // Arrange
        decimal grossIncome = 1000m;

        // Act
        var tax = _strategy.Calculate(grossIncome);

        // Assert
        Assert.Equal(225m, tax);
    }


}
