using CDBCalculation.Domain.Interface;
using CDBCalculation.Domain.Service;
using CDBCalculation.Domain.TaxCalculatorStrategies;
using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;
using Moq;

namespace CDBCalculation.Domain.Tests.Service;

public class CdbCalculationServiceTests
{

    private readonly Mock<ICdbCalculationValidator> _validatorMock;
    private readonly CdbCalculationService _service;


    public CdbCalculationServiceTests()
    {
        _validatorMock = new Mock<ICdbCalculationValidator>();


        var strategy = new ITaxCalculatorStrategy[] // Initialize strategies List
         {
            new Upto6MonthsTaxStrategy(),
            new Upto12MonthsTaxStrategy(),
            new Upto24MonthsTaxStrategy(),
            new Over24MonthsTaxStrategy()
        };

        var strategyContext = new TaxCalculatorStrategyContext(strategy);


        _service = new CdbCalculationService(_validatorMock.Object, strategyContext);

    }


    [Theory]
    [InlineData(0, 4)]
    [InlineData(-10, 6)]
    [InlineData(-5, 2)]
    public async Task DoCDBCalculation_Should_Fail_When_InitialValue_IsNot_Positive(decimal InitialValue, int termMonths)
    {
        // Arrange
        _validatorMock
             .Setup(v => v.Validate(InitialValue, termMonths))
             .Returns(Result<CdbCalculationResult>.Failure("RedemptionValue must be positive"));


        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(InitialValue, termMonths));


        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
        Assert.Null(result.Value);


        _validatorMock.Verify(
       v => v.Validate(InitialValue, termMonths),
       Times.Once);


    }

    [Theory]
    [InlineData(1, -1)]
    [InlineData(5, 0)]
    public async Task DoCDBCalculation_Should_Fail_When_TermMonths_IsNot_Greater_Than_One(decimal redemptionValue, int termMonths)
    {
        // Arrange
        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.Failure("termMonths must be greater than 1"));


        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));


        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
        Assert.Null(result.Value);


        _validatorMock.Verify(
       v => v.Validate(redemptionValue, termMonths),
       Times.Once);


    }


    [Fact]
    public async Task DoCDBCalculation_Should_Sucess_When_Validation_Is_Valid()
    {
        // Arrange
        _validatorMock
            .Setup(v => v.Validate(10m, 6))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(10M, 6));


        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<CdbCalculationResult>(result.Value);
        Assert.Null(result.Error);


        _validatorMock.Verify(
            v => v.Validate(10m, 6),
            Times.Once);

    }

    [Fact]
    public async Task DoCDBCalculation_Should_Return_GenericError_When_ValidationError_Is_Null()
    {
        // Arrange
        _validatorMock
            .Setup(v => v.Validate(1000m, 12))
            .Returns(Result<CdbCalculationResult>.Failure(null!));

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(1000m, 12));

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Validation error occurred.", result.Error);
        Assert.Null(result.Value);
    }

    
    [Fact]
    public async Task DoCDBCalculation_Should_Calculate_GrossValue_Correctly_For_One_Month()
    {
        // Arrange
        decimal redemptionValue = 1000.00m;
        int termMonths = 1;

        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

   
        decimal expectedGrossValue = 1009.72m;
        Assert.Equal(expectedGrossValue, result.Value.GrossValue, 2);
    }

    [Fact]
    public async Task DoCDBCalculation_Should_Calculate_GrossValue_Correctly_For_Twelve_Months()
    {
        // Arrange
        decimal redemptionValue = 1000.00m;
        int termMonths = 12;

        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

     
        decimal expectedGrossValue = 1123.08m;
        Assert.Equal(expectedGrossValue, result.Value.GrossValue, 2);
    }

    [Fact]
    public async Task DoCDBCalculation_Should_Round_Values_To_Two_Decimal_Places()
    {
        // Arrange
        decimal redemptionValue = 1000.00m;
        int termMonths = 2;

        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        
        var grossValueString = result.Value.GrossValue.ToString("F2");
        var netValueString = result.Value.NetWorth.ToString("F2");

        Assert.Equal(result.Value.GrossValue, decimal.Parse(grossValueString));
        Assert.Equal(result.Value.NetWorth, decimal.Parse(netValueString));
    }

    

    [Fact]
    public async Task DoCDBCalculation_Should_Calculate_NetValue_As_GrossValue_Minus_Tax()
    {
        // Arrange
        decimal redemptionValue = 1000.00m;
        int termMonths = 6; 

        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        
        decimal expectedNetValue = result.Value.GrossValue * 0.775m;
        expectedNetValue = Math.Round(expectedNetValue, 2);

        Assert.Equal(expectedNetValue, result.Value.NetWorth, 2);
    }

    [Fact]
    public async Task DoCDBCalculation_Should_Use_Upto6MonthsTaxStrategy_For_Months_1_To_6()
    {
        // Arrange
        decimal redemptionValue = 1000.00m;
        int termMonths = 3; 

        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        
        decimal tax = result.Value.GrossValue - result.Value.NetWorth;
        decimal taxPercentage = (tax / result.Value.GrossValue) * 100m;
        Assert.Equal(22.5m, taxPercentage, 1);
    }

    [Fact]
    public async Task DoCDBCalculation_Should_Use_Upto12MonthsTaxStrategy_For_Months_7_To_12()
    {
        // Arrange
        decimal redemptionValue = 1000.00m;
        int termMonths = 9; 

        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        
        decimal tax = result.Value.GrossValue - result.Value.NetWorth;
        decimal taxPercentage = (tax / result.Value.GrossValue) * 100m;
        Assert.Equal(20.0m, taxPercentage, 1);
    }

    [Fact]
    public async Task DoCDBCalculation_Should_Use_Upto24MonthsTaxStrategy_For_Months_13_To_24()
    {
        // Arrange
        decimal redemptionValue = 1000.00m;
        int termMonths = 18; 

        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        
        decimal tax = result.Value.GrossValue - result.Value.NetWorth;
        decimal taxPercentage = (tax / result.Value.GrossValue) * 100m;
        Assert.Equal(17.5m, taxPercentage, 1);
    }

    [Fact]
    public async Task DoCDBCalculation_Should_Use_Over24MonthsTaxStrategy_For_Months_Greater_Than_24()
    {
        // Arrange
        decimal redemptionValue = 1000.00m;
        int termMonths = 36; 

        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        
        decimal tax = result.Value.GrossValue - result.Value.NetWorth;
        decimal taxPercentage = (tax / result.Value.GrossValue) * 100m;
        Assert.Equal(15.0m, taxPercentage, 1);
    }

   

    [Fact]
    public async Task DoCDBCalculation_Should_Return_Result_With_Correct_Structure()
    {
        // Arrange
        decimal redemptionValue = 1000.00m;
        int termMonths = 6;

        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<CdbCalculationResult>(result.Value);
        Assert.True(result.Value.GrossValue > 0);
        Assert.True(result.Value.NetWorth > 0);
        Assert.True(result.Value.NetWorth < result.Value.GrossValue);
    }

    [Theory]
    [InlineData(2, 1000.00)]
    [InlineData(6, 1000.00)]
    [InlineData(12, 1000.00)]
    [InlineData(24, 1000.00)]
    [InlineData(36, 1000.00)]
    public async Task DoCDBCalculation_Should_Increase_GrossValue_With_More_Months(
        int termMonths,
        decimal redemptionValue)
    {
        // Arrange
        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        // Quanto mais meses, maior deve ser o valor bruto
        decimal monthlyRate = 0.009m * 1.08m;
        decimal minExpectedValue = redemptionValue * (1 + monthlyRate);

        Assert.True(result.Value.GrossValue >= minExpectedValue);
    }

    [Fact]
    public async Task DoCDBCalculation_Should_Handle_Large_Values()
    {
        // Arrange
        decimal redemptionValue = 1000000.00m;
        int termMonths = 12;

        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.True(result.Value.GrossValue > redemptionValue);
        Assert.True(result.Value.NetWorth > 0);
    }

    

    [Fact]
    public async Task DoCDBCalculation_Should_Calculate_Correctly_For_Exact_Boundary_Values()
    {
        // Arrange - 
        decimal redemptionValue = 1000.00m;
        int termMonths = 6; 

        _validatorMock
            .Setup(v => v.Validate(redemptionValue, termMonths))
            .Returns(Result<CdbCalculationResult>.SuccessValidation());

        // Act
        var result = await _service.DoCDBCalculation(new CdbCalculation(redemptionValue, termMonths));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        
        decimal tax = result.Value.GrossValue - result.Value.NetWorth;
        decimal taxPercentage = (tax / result.Value.GrossValue) * 100m;
        Assert.Equal(22.5m, taxPercentage, 1);
    }
}
