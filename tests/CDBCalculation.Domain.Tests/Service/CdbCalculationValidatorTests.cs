using CDBCalculation.Domain.Service;

namespace CDBCalculation.Domain.Tests;

public class CdbCalculationValidatorTests
{
    private readonly CdbCalculationValidator _validator;

    public CdbCalculationValidatorTests()
    {
        _validator = new CdbCalculationValidator();
    }

    [Fact]
    public void Validate_WhenValueIsZero_ShouldReturnFailure()
    {
        // Arrange
        decimal value = 0;
        int termMonths = 12;

        // Act
        var result = _validator.Validate(value, termMonths);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("The value must be a positive number.", result.Error);
    }

    [Fact]
    public void Validate_WhenValueIsNegative_ShouldReturnFailure()
    {
        // Arrange
        decimal value = -100.50m;
        int termMonths = 12;

        // Act
        var result = _validator.Validate(value, termMonths);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("The value must be a positive number.", result.Error);
    }

    [Fact]
    public void Validate_WhenValueIsPositiveButTermMonthsIsOne_ShouldReturnFailure()
    {
        // Arrange
        decimal value = 1000.00m;
        int termMonths = 1;

        // Act
        var result = _validator.Validate(value, termMonths);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("The TermMonths must be greater than one month.", result.Error);
    }

    [Fact]
    public void Validate_WhenValueIsPositiveButTermMonthsIsZero_ShouldReturnFailure()
    {
        // Arrange
        decimal value = 1000.00m;
        int termMonths = 0;

        // Act
        var result = _validator.Validate(value, termMonths);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("The TermMonths must be greater than one month.", result.Error);
    }

    [Fact]
    public void Validate_WhenValueIsPositiveButTermMonthsIsNegative_ShouldReturnFailure()
    {
        // Arrange
        decimal value = 1000.00m;
        int termMonths = -5;

        // Act
        var result = _validator.Validate(value, termMonths);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("The TermMonths must be greater than one month.", result.Error);
    }

    [Fact]
    public void Validate_WhenValueIsZeroAndTermMonthsIsOne_ShouldReturnFailureForValue()
    {
        // Arrange
        decimal value = 0;
        int termMonths = 1;

        // Act
        var result = _validator.Validate(value, termMonths);

        // Assert
        // A validação verifica value primeiro, então deve retornar erro de value
        Assert.False(result.IsSuccess);
        Assert.Equal("The value must be a positive number.", result.Error);
    }

    [Fact]
    public void Validate_WhenValueIsPositiveAndTermMonthsIsGreaterThanOne_ShouldReturnSuccess()
    {
        // Arrange
        decimal value = 1000.00m;
        int termMonths = 12;

        // Act
        var result = _validator.Validate(value, termMonths);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Validate_WhenValueIsMinimumPositiveAndTermMonthsIsMinimum_ShouldReturnSuccess()
    {
        // Arrange
        decimal value = 0.01m;
        int termMonths = 2;

        // Act
        var result = _validator.Validate(value, termMonths);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Validate_WhenValueIsLargeAndTermMonthsIsLarge_ShouldReturnSuccess()
    {
        // Arrange
        decimal value = 1000000.99m;
        int termMonths = 60;

        // Act
        var result = _validator.Validate(value, termMonths);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Validate_WhenValueIsPositiveButTermMonthsIsExactlyOne_ShouldReturnFailure()
    {
        // Arrange
        decimal value = 5000.00m;
        int termMonths = 1;

        // Act
        var result = _validator.Validate(value, termMonths);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("The TermMonths must be greater than one month.", result.Error);
    }

    [Fact]
    public void Validate_WhenValueIsPositiveAndTermMonthsIsExactlyTwo_ShouldReturnSuccess()
    {
        // Arrange
        decimal value = 5000.00m;
        int termMonths = 2;

        // Act
        var result = _validator.Validate(value, termMonths);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Error);
    }
}

