using CDBCalculation.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace CDBCalculation.Application.Tests.Dtos
{
    public class CdbCalculationRequestDtoTests
    {

        private static IList<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);

            Validator.TryValidateObject(
                model,
                context,
                results,
                validateAllProperties: true);

            return results;
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void ShouldBeInvalid_WhenInitialValueIsZeroOrNegative(decimal value)
        {
            var dto = new CdbCalculationRequestDto
            {
                RedemptionValue = value,
                TermMonths = 12
            };

            var results = ValidateModel(dto);

            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-5)]
        public void ShouldBeInvalid_WhenTermMonthsIsLessThanTwo(int months)
        {
            var dto = new CdbCalculationRequestDto
            {
                RedemptionValue = 1000,
                TermMonths = months
            };

            var results = ValidateModel(dto);

            Assert.NotEmpty(results);
        }

        [Fact]
        public void ShouldBeValid_WhenValuesAreCorrect()
        {
            var dto = new CdbCalculationRequestDto
            {
                RedemptionValue = 1000,
                TermMonths = 12
            };

            var results = ValidateModel(dto);

            Assert.Empty(results);
        }

    }
}
