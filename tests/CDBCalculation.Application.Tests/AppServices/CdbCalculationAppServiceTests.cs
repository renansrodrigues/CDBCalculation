using CDBCalculation.Application.AppServices;
using CDBCalculation.Application.DTOs;
using CDBCalculation.Domain.Interface;
using CDBCalculation.Domain.ValueObjects.Shared;
using CDBCalculation.Domain.ValueObjects;
using Moq;

namespace CDBCalculation.Application.Tests.AppServices;


public class CdbCalculationAppServiceTests
{
    private readonly Mock<ICdbCalculationService> _serviceMock;
    private readonly CdbCalculationAppService _appService;


    public CdbCalculationAppServiceTests()
    {
        _serviceMock = new Mock<ICdbCalculationService>();

        _appService = new CdbCalculationAppService(
            _serviceMock.Object);
    }

    [Fact]
    public async Task DoCDBCalculation_ShouldReturnSuccess_WhenCalculationIsValid()
    {
        // Arrange
        var dto = new CdbCalculationRequestDto
        {
            RedemptionValue = 1000,
            TermMonths = 12
        };

        var expectedResult = Result<CdbCalculationResult>.Success(
            new CdbCalculationResult(1123.08M, 898.47M));        

        _serviceMock
        .Setup(s => s.DoCDBCalculation(It.Is<CdbCalculation>(c =>
        c.RedemptionValue == 1000M &&
        c.TermMonths == 12)))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _appService.DoCDBCalculation(dto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        _serviceMock.Verify(
    s => s.DoCDBCalculation(It.Is<CdbCalculation>(c =>
        c.RedemptionValue == 1000M &&
        c.TermMonths == 12)),
             Times.Once);
    }
    [Fact]
    public async Task DoCDBCalculation_ShouldReturnFailure_WhenServiceReturnsFailure()
    {
        // Arrange
        var dto = new CdbCalculationRequestDto
        {
            RedemptionValue = 0,
            TermMonths = 1
        };

        var failure = Result<CdbCalculationResult>.Failure(
            "Invalid Value");
        

        _serviceMock
        .Setup(s => s.DoCDBCalculation(It.Is<CdbCalculation>(c =>
        c.RedemptionValue == 0 &&
        c.TermMonths == 1)))
            .ReturnsAsync(failure);        

        // Act
        var result = await _appService.DoCDBCalculation(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid Value", result.Error);


        _serviceMock.Verify(
   s => s.DoCDBCalculation(It.Is<CdbCalculation>(c =>
       c.RedemptionValue == 0 &&
       c.TermMonths == 1)),
            Times.Once);

     
    }

}
