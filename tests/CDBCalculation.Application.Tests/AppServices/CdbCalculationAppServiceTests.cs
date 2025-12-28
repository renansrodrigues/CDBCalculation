using CDBCalculation.Application.AppServices;
using CDBCalculation.Application.DTOs;
using CDBCalculation.Domain.Interface;
using CDBCalculation.Domain.ValueObjects.Shared;
using CDBCalculation.Domain.ValueObjects;
using Moq;

namespace CDBCalculation.Application.Tests.AppServices;


public class CdbCalculationAppServiceTests
{
    private readonly Mock<ICdbCalculationService> _cdbCalculationServiceMock;
    private readonly CdbCalculationAppService _appService;

    public CdbCalculationAppServiceTests()
    {
        _cdbCalculationServiceMock = new Mock<ICdbCalculationService>();
        _appService = new CdbCalculationAppService(_cdbCalculationServiceMock.Object);
    }

    [Fact]
    public async Task DoCDBCalculation_WhenServiceReturnsSuccess_ShouldReturnSuccessResult()
    {
        // Arrange
        var requestDto = new CdbCalculationRequestDto
        {
            RedemptionValue = 1000.00m,
            TermMonths = 12
        };

        var domainResult = new CdbCalculationResult(1100.00m, 1080.00m);
        var serviceResult = Result<CdbCalculationResult>.Success(domainResult);

        _cdbCalculationServiceMock
            .Setup(x => x.DoCDBCalculation(It.IsAny<CdbCalculation>()))
            .ReturnsAsync(serviceResult);

        // Act
        var result = await _appService.DoCDBCalculation(requestDto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(1100.00m, result.Value.GrossValue);
        Assert.Equal(1080.00m, result.Value.NetWorth);
        Assert.Null(result.Error);

        _cdbCalculationServiceMock.Verify(
            x => x.DoCDBCalculation(It.Is<CdbCalculation>(
                c => c.RedemptionValue == requestDto.RedemptionValue &&
                     c.TermMonths == requestDto.TermMonths)),
            Times.Once);
    }

    [Fact]
    public async Task DoCDBCalculation_WhenServiceReturnsFailure_ShouldReturnFailureResult()
    {
        // Arrange
        var requestDto = new CdbCalculationRequestDto
        {
            RedemptionValue = 1000.00m,
            TermMonths = 12
        };

        var errorMessage = "Invalid calculation parameters";
        var serviceResult = Result<CdbCalculationResult>.Failure(errorMessage);

        _cdbCalculationServiceMock
            .Setup(x => x.DoCDBCalculation(It.IsAny<CdbCalculation>()))
            .ReturnsAsync(serviceResult);

        // Act
        var result = await _appService.DoCDBCalculation(requestDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Equal(errorMessage, result.Error);
    }

    [Fact]
    public async Task DoCDBCalculation_WhenServiceReturnsNullResult_ShouldReturnFailureResult()
    {
        // Arrange
        var requestDto = new CdbCalculationRequestDto
        {
            RedemptionValue = 1000.00m,
            TermMonths = 12
        };

        Result<CdbCalculationResult>? serviceResult = null;

        _cdbCalculationServiceMock
            .Setup(x => x.DoCDBCalculation(It.IsAny<CdbCalculation>()))
            .ReturnsAsync(serviceResult!);

        // Act
        var result = await _appService.DoCDBCalculation(requestDto);

        // Assert
        // Quando o resultado é null, ocorre NullReferenceException ao acessar result.IsSuccess
        // que é capturada pelo catch e retorna a mensagem da exceção
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Contains("Object reference", result.Error ?? string.Empty);
    }


    [Fact]
    public async Task DoCDBCalculation_WhenServiceThrowsException_ShouldReturnFailureResult()
    {
        // Arrange
        var requestDto = new CdbCalculationRequestDto
        {
            RedemptionValue = 1000.00m,
            TermMonths = 12
        };

        var exceptionMessage = "Service unavailable";
        _cdbCalculationServiceMock
            .Setup(x => x.DoCDBCalculation(It.IsAny<CdbCalculation>()))
            .ThrowsAsync(new Exception(exceptionMessage));

        // Act
        var result = await _appService.DoCDBCalculation(requestDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Equal(exceptionMessage, result.Error);
    }

    [Fact]
    public async Task DoCDBCalculation_ShouldConvertDtoToDomainCorrectly()
    {
        // Arrange
        var requestDto = new CdbCalculationRequestDto
        {
            RedemptionValue = 5000.50m,
            TermMonths = 24
        };

        var domainResult = new CdbCalculationResult(5500.00m, 5400.00m);
        var serviceResult = Result<CdbCalculationResult>.Success(domainResult);

        _cdbCalculationServiceMock
            .Setup(x => x.DoCDBCalculation(It.IsAny<CdbCalculation>()))
            .ReturnsAsync(serviceResult);

        // Act
        await _appService.DoCDBCalculation(requestDto);

        // Assert
        _cdbCalculationServiceMock.Verify(
            x => x.DoCDBCalculation(It.Is<CdbCalculation>(
                c => c.RedemptionValue == 5000.50m &&
                     c.TermMonths == 24)),
            Times.Once);
    }

    [Fact]
    public async Task DoCDBCalculation_ShouldConvertDomainResultToDtoCorrectly()
    {
        // Arrange
        var requestDto = new CdbCalculationRequestDto
        {
            RedemptionValue = 2000.00m,
            TermMonths = 6
        };

        var grossValue = 2100.00m;
        var netWorth = 2080.00m;
        var domainResult = new CdbCalculationResult(grossValue, netWorth);
        var serviceResult = Result<CdbCalculationResult>.Success(domainResult);

        _cdbCalculationServiceMock
            .Setup(x => x.DoCDBCalculation(It.IsAny<CdbCalculation>()))
            .ReturnsAsync(serviceResult);

        // Act
        var result = await _appService.DoCDBCalculation(requestDto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(grossValue, result.Value.GrossValue);
        Assert.Equal(netWorth, result.Value.NetWorth);
    }
}
