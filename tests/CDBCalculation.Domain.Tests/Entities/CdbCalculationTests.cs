
using CDBCalculation.Domain.ValueObjects;

namespace CDBCalculation.Domain.Tests.Entities
{
    public class CdbCalculationTests
    {

        [Fact]
        public void Constructor_ShouldAssignValuesCorrectly()
        {
            // Arrange
            var redemptionValue = 1000m;
            var termMonths = 12;

            // Act
            var calculation = new CdbCalculation(
                redemptionValue,
                termMonths);

            // Assert
            Assert.Equal(redemptionValue, calculation.RedemptionValue);
            Assert.Equal(termMonths, calculation.TermMonths);
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(1000, 1)]
        [InlineData(5000.50, 24)]
        public void ShouldAcceptDifferentValidValues(
         decimal redemptionValue,
         int termMonths)
        {
            // Act
            var calculation = new CdbCalculation(
                redemptionValue,
                termMonths);

            // Assert
            Assert.Equal(redemptionValue, calculation.RedemptionValue);
            Assert.Equal(termMonths, calculation.TermMonths);
        }

    }
}
