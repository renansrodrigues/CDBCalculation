using CDBCalculation.Application.DTOs;
using CDBCalculation.Domain.ValueObjects;

namespace CDBCalculation.Application.Tests.Extensions
{
    public class CdbCalculationResponseDtoExtensionsTests
    {

        [Fact]
        public void ResponseToDto_ShouldMapValuesCorrectly()
        {
            // Arrange
            var domain = new CdbCalculationResult(
                GrossValue: 1500.75m,
                NetWorth: 1320.50m);

            // Act
            var dto = CdbCalculationResponseDtoExtensions.ResponseToDto(domain);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(domain.GrossValue, dto.GrossValue);
            Assert.Equal(domain.NetWorth, dto.NetWorth);
        }
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1000, 950)]
        [InlineData(5000.25, 4800.10)]
        public void ResponseToDto_ShouldHandleDifferentValues(
        decimal grossValue,
        decimal netWorth)
        {
            // Arrange
            var domain = new CdbCalculationResult(grossValue, netWorth);

            // Act
            var dto = CdbCalculationResponseDtoExtensions.ResponseToDto(domain);

            // Assert
            Assert.Equal(grossValue, dto.GrossValue);
            Assert.Equal(netWorth, dto.NetWorth);
        }


    }
}
