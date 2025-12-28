using CDBCalculation.Application.DTOs;
using CDBCalculation.Domain.ValueObjects;

namespace CDBCalculation.Application.Tests.Extensions
{
    public class CdbCalculationRequestDtoExtensionsTests
    {


        [Fact]
        public void DtoToDomain_ShouldReturn_Correctly_Type()
        {
            // Arrange
            var dto = new CdbCalculationRequestDto { RedemptionValue = 1000m, TermMonths = 12 };

            // Act
            var domain = dto.DtoToDomain();

            // Assert
            Assert.NotNull(domain);
            Assert.IsType<CdbCalculation>(domain);
        }



        [Fact]
        public void DtoToDomain_ShouldMapCorrectly()
        {
            // Arrange
            var dto = new CdbCalculationRequestDto { RedemptionValue = 1000m, TermMonths = 12 };

            // Act
            var domain = dto.DtoToDomain();

            // Assert
            Assert.NotNull(domain);
            Assert.Equal(1000m, domain.RedemptionValue);
            Assert.Equal(12, domain.TermMonths);
        }


    }
}
