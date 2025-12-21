using CDBCalculation.Domain.TaxCalculatorStrategies;

namespace CDBCalculation.Domain.Tests.TaxCalculatorStrategies;

public class Upto12MonthsTaxStrategyTests
{

    private readonly Upto12MonthsTaxStrategy _strategy;
    public Upto12MonthsTaxStrategyTests()
    {
        _strategy = new Upto12MonthsTaxStrategy();
    }


    [Theory]
    [InlineData(7)]
    [InlineData(12)]
    public void IsApplicable_ShouldReturnTrue_WhenMonthsBetween7And12(int months)
    {
        // Act
        var result = _strategy.IsApplicable(months);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(13)]
    [InlineData(16)]
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
        Assert.Equal(200m, tax);
    }





}
