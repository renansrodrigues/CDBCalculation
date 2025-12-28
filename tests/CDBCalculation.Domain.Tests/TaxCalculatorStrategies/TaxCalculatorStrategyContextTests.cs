using CDBCalculation.Domain.TaxCalculatorStrategies;
using CDBCalculation.Domain.ValueObjects.Shared;
using Moq;

namespace CDBCalculation.Domain.Tests;

public class TaxCalculatorStrategyContextTests
{
    [Fact]
    public void ExecuteStrategy_WhenStrategyIsApplicable_ShouldReturnSuccessWithCalculatedTax()
    {
        // Arrange
        var months = 6;
        var incomeValue = 1000.00m;
        var expectedTax = 225.00m; // 22.5% de 1000

        var strategyMock = new Mock<ITaxCalculatorStrategy>();
        strategyMock
            .Setup(s => s.IsApplicable(months))
            .Returns(true);
        strategyMock
            .Setup(s => s.Calculate(incomeValue))
            .Returns(expectedTax);

        var strategies = new[] { strategyMock.Object };
        var context = new TaxCalculatorStrategyContext(strategies);

        // Act
        var result = context.ExecuteStrategy(months, incomeValue);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedTax, result.Value);
        Assert.Null(result.Error);

        strategyMock.Verify(s => s.IsApplicable(months), Times.Once);
        strategyMock.Verify(s => s.Calculate(incomeValue), Times.Once);
    }

    [Fact]
    public void ExecuteStrategy_WhenNoStrategyIsApplicable_ShouldReturnFailure()
    {
        // Arrange
        var months = 100;
        var incomeValue = 1000.00m;

        var strategyMock = new Mock<ITaxCalculatorStrategy>();
        strategyMock
            .Setup(s => s.IsApplicable(months))
            .Returns(false);

        var strategies = new[] { strategyMock.Object };
        var context = new TaxCalculatorStrategyContext(strategies);

        // Act
        var result = context.ExecuteStrategy(months, incomeValue);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(default(decimal), result.Value);
        Assert.Equal("There is no Tax.", result.Error);

        strategyMock.Verify(s => s.IsApplicable(months), Times.Once);
        strategyMock.Verify(s => s.Calculate(It.IsAny<decimal>()), Times.Never);
    }

    [Fact]
    public void ExecuteStrategy_WhenMultipleStrategies_ShouldUseFirstApplicable()
    {
        // Arrange
        var months = 12;
        var incomeValue = 1000.00m;
        var firstTax = 200.00m;
        var secondTax = 175.00m;

        var firstStrategyMock = new Mock<ITaxCalculatorStrategy>();
        firstStrategyMock
            .Setup(s => s.IsApplicable(months))
            .Returns(true);
        firstStrategyMock
            .Setup(s => s.Calculate(incomeValue))
            .Returns(firstTax);

        var secondStrategyMock = new Mock<ITaxCalculatorStrategy>();
        secondStrategyMock
            .Setup(s => s.IsApplicable(months))
            .Returns(true);
        secondStrategyMock
            .Setup(s => s.Calculate(incomeValue))
            .Returns(secondTax);

        var strategies = new[] { firstStrategyMock.Object, secondStrategyMock.Object };
        var context = new TaxCalculatorStrategyContext(strategies);

        // Act
        var result = context.ExecuteStrategy(months, incomeValue);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(firstTax, result.Value); // Deve usar a primeira estratégia aplicável

        firstStrategyMock.Verify(s => s.IsApplicable(months), Times.Once);
        firstStrategyMock.Verify(s => s.Calculate(incomeValue), Times.Once);

        // A segunda estratégia não deve ser chamada porque a primeira já foi aplicável
        secondStrategyMock.Verify(s => s.IsApplicable(months), Times.Never);
        secondStrategyMock.Verify(s => s.Calculate(It.IsAny<decimal>()), Times.Never);
    }

    [Fact]
    public void ExecuteStrategy_WhenFirstStrategyNotApplicableButSecondIs_ShouldUseSecondStrategy()
    {
        // Arrange
        var months = 12;
        var incomeValue = 1000.00m;
        var secondTax = 175.00m;

        var firstStrategyMock = new Mock<ITaxCalculatorStrategy>();
        firstStrategyMock
            .Setup(s => s.IsApplicable(months))
            .Returns(false);

        var secondStrategyMock = new Mock<ITaxCalculatorStrategy>();
        secondStrategyMock
            .Setup(s => s.IsApplicable(months))
            .Returns(true);
        secondStrategyMock
            .Setup(s => s.Calculate(incomeValue))
            .Returns(secondTax);

        var strategies = new[] { firstStrategyMock.Object, secondStrategyMock.Object };
        var context = new TaxCalculatorStrategyContext(strategies);

        // Act
        var result = context.ExecuteStrategy(months, incomeValue);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(secondTax, result.Value);

        firstStrategyMock.Verify(s => s.IsApplicable(months), Times.Once);
        firstStrategyMock.Verify(s => s.Calculate(It.IsAny<decimal>()), Times.Never);

        secondStrategyMock.Verify(s => s.IsApplicable(months), Times.Once);
        secondStrategyMock.Verify(s => s.Calculate(incomeValue), Times.Once);
    }

    [Fact]
    public void ExecuteStrategy_WhenStrategiesListIsEmpty_ShouldReturnFailure()
    {
        // Arrange
        var months = 6;
        var incomeValue = 1000.00m;
        var strategies = Array.Empty<ITaxCalculatorStrategy>();
        var context = new TaxCalculatorStrategyContext(strategies);

        // Act
        var result = context.ExecuteStrategy(months, incomeValue);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(default(decimal), result.Value);
        Assert.Equal("There is no Tax.", result.Error);
    }

    [Fact]
    public void ExecuteStrategy_ShouldPassCorrectIncomeValueToCalculate()
    {
        // Arrange
        var months = 6;
        var incomeValue = 5000.50m;
        var expectedTax = 1125.1125m; // 22.5% de 5000.50

        var strategyMock = new Mock<ITaxCalculatorStrategy>();
        strategyMock
            .Setup(s => s.IsApplicable(months))
            .Returns(true);
        strategyMock
            .Setup(s => s.Calculate(incomeValue))
            .Returns(expectedTax);

        var strategies = new[] { strategyMock.Object };
        var context = new TaxCalculatorStrategyContext(strategies);

        // Act
        var result = context.ExecuteStrategy(months, incomeValue);

        // Assert
        Assert.True(result.IsSuccess);
        strategyMock.Verify(s => s.Calculate(incomeValue), Times.Once);
    }

    [Fact]
    public void ExecuteStrategy_WithDifferentMonths_ShouldCallIsApplicableWithCorrectMonths()
    {
        // Arrange
        var months = 24;
        var incomeValue = 1000.00m;
        var expectedTax = 175.00m; // 17.5% de 1000

        var strategyMock = new Mock<ITaxCalculatorStrategy>();
        strategyMock
            .Setup(s => s.IsApplicable(months))
            .Returns(true);
        strategyMock
            .Setup(s => s.Calculate(incomeValue))
            .Returns(expectedTax);

        var strategies = new[] { strategyMock.Object };
        var context = new TaxCalculatorStrategyContext(strategies);

        // Act
        var result = context.ExecuteStrategy(months, incomeValue);

        // Assert
        Assert.True(result.IsSuccess);
        strategyMock.Verify(s => s.IsApplicable(months), Times.Once);
    }

    [Fact]
    public void ExecuteStrategy_WhenAllStrategiesNotApplicable_ShouldReturnFailure()
    {
        // Arrange
        var months = 100;
        var incomeValue = 1000.00m;

        var strategy1Mock = new Mock<ITaxCalculatorStrategy>();
        strategy1Mock
            .Setup(s => s.IsApplicable(months))
            .Returns(false);

        var strategy2Mock = new Mock<ITaxCalculatorStrategy>();
        strategy2Mock
            .Setup(s => s.IsApplicable(months))
            .Returns(false);

        var strategy3Mock = new Mock<ITaxCalculatorStrategy>();
        strategy3Mock
            .Setup(s => s.IsApplicable(months))
            .Returns(false);

        var strategies = new[]
        {
            strategy1Mock.Object,
            strategy2Mock.Object,
            strategy3Mock.Object
        };
        var context = new TaxCalculatorStrategyContext(strategies);

        // Act
        var result = context.ExecuteStrategy(months, incomeValue);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(default(decimal), result.Value);
        Assert.Equal("There is no Tax.", result.Error);

        // Todas as estratégias devem ser verificadas
        strategy1Mock.Verify(s => s.IsApplicable(months), Times.Once);
        strategy2Mock.Verify(s => s.IsApplicable(months), Times.Once);
        strategy3Mock.Verify(s => s.IsApplicable(months), Times.Once);
    }

    [Fact]
    public void ExecuteStrategy_WithRealStrategies_ShouldWorkCorrectly()
    {
        // Arrange
        var upto6MonthsStrategy = new Upto6MonthsTaxStrategy();
        var upto12MonthsStrategy = new Upto12MonthsTaxStrategy();
        var upto24MonthsStrategy = new Upto24MonthsTaxStrategy();
        var over24MonthsStrategy = new Over24MonthsTaxStrategy();

        var strategies = new ITaxCalculatorStrategy[]
        {
            upto6MonthsStrategy,
            upto12MonthsStrategy,
            upto24MonthsStrategy,
            over24MonthsStrategy
        };

        var context = new TaxCalculatorStrategyContext(strategies);

        // Act & Assert - Teste para 3 meses (deve usar Upto6MonthsTaxStrategy - 22.5%)
        var result3Months = context.ExecuteStrategy(3, 1000.00m);
        Assert.True(result3Months.IsSuccess);
        Assert.Equal(225.00m, result3Months.Value); // 22.5% de 1000

        // Act & Assert - Teste para 9 meses (deve usar Upto12MonthsTaxStrategy - 20%)
        var result9Months = context.ExecuteStrategy(9, 1000.00m);
        Assert.True(result9Months.IsSuccess);
        Assert.Equal(200.00m, result9Months.Value); // 20% de 1000

        // Act & Assert - Teste para 18 meses (deve usar Upto24MonthsTaxStrategy - 17.5%)
        var result18Months = context.ExecuteStrategy(18, 1000.00m);
        Assert.True(result18Months.IsSuccess);
        Assert.Equal(175.00m, result18Months.Value); // 17.5% de 1000

        // Act & Assert - Teste para 36 meses (deve usar Over24MonthsTaxStrategy - 15%)
        var result36Months = context.ExecuteStrategy(36, 1000.00m);
        Assert.True(result36Months.IsSuccess);
        Assert.Equal(150.00m, result36Months.Value); // 15% de 1000
    }
}

