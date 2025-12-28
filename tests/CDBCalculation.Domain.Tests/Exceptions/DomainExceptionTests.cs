using CDBCalculation.Domain.Exceptions;

namespace CDBCalculation.Domain.Tests.Exceptions;

public class DomainExceptionTests
{
    [Fact]
    public void ShouldCreateDomainException_WithMessage()
    {
        var ex = new DomainException("Invalid domain state");

        Assert.Equal("Invalid domain state", ex.Message);
    }


}
