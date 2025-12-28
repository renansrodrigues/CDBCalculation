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
        

        var strategy =  new ITaxCalculatorStrategy[] // Initialize strategies List
         {
            new Upto6MonthsTaxStrategy(),
            new Upto12MonthsTaxStrategy(),
            new Upto24MonthsTaxStrategy(),
            new Over24MonthsTaxStrategy()
        };

        var strategyContext = new TaxCalculatorStrategyContext(strategy);


        _service = new CdbCalculationService(_validatorMock.Object,strategyContext);

    }


    [Theory]
    [InlineData(0,4)]
    [InlineData(-10,6)]
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





}
