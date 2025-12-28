using CDBCalculation.Application.DTOs;

namespace CDBCalculation.Application.Tests.Dtos
{
    public class CdbCalculationResponseDtoTests
    {

        [Fact]
        public void ShouldAssignValuesCorrectly()
        {
            // Arrange
            var grossValue = 1500.75m;
            var netWorth = 1320.50m;

            // Act
            var dto = new CdbCalculationResponseDto(grossValue, netWorth);

            // Assert
            Assert.Equal(grossValue, dto.GrossValue);
            Assert.Equal(netWorth, dto.NetWorth);
        }

        [Fact]
        public void Records_WithSameValues_ShouldBeEqual()
        {
            // Arrange
            var dto1 = new CdbCalculationResponseDto(2000, 1800);
            var dto2 = new CdbCalculationResponseDto(2000, 1800);

            // Act & Assert
            Assert.Equal(dto1, dto2);
        }

        [Fact]
        public void Records_WithDifferentValues_ShouldNotBeEqual()
        {
            // Arrange
            var dto1 = new CdbCalculationResponseDto(2000, 1800);
            var dto2 = new CdbCalculationResponseDto(2500, 2000);

            // Act & Assert
            Assert.NotEqual(dto1, dto2);
        }

    }



}
