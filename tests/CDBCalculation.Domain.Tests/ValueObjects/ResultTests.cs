using CDBCalculation.Domain.ValueObjects;
using CDBCalculation.Domain.ValueObjects.Shared;

namespace CDBCalculation.Domain.Tests.ValueObjects.Shared;

public class ResultTests
{
    [Fact]
    public void Success_Should_Create_Successful_Result_With_Value()
    {
        // Arrange
        var value = new CdbCalculationResult(1000.00m, 900.00m);

        // Act
        var result = Result<CdbCalculationResult>.Success(value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(value, result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Success_Should_Create_Successful_Result_With_Value_Type()
    {
        // Arrange
        int value = 42;

        // Act
        var result = Result<int>.Success(value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Success_Should_Create_Successful_Result_With_String()
    {
        // Arrange
        string value = "Test String";

        // Act
        var result = Result<string>.Success(value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Success_Should_Create_Successful_Result_With_Null_When_T_Is_Nullable()
    {
        // Arrange
        string? value = null;

        // Act
        var result = Result<string?>.Success(value!);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void SuccessValidation_Should_Create_Successful_Result_With_Default_Value()
    {
        // Act
        var result = Result<CdbCalculationResult>.SuccessValidation();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void SuccessValidation_Should_Create_Successful_Result_With_Default_Value_For_Value_Type()
    {
        // Act
        var result = Result<int>.SuccessValidation();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(default(int), result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void SuccessValidation_Should_Create_Successful_Result_With_Default_Value_For_Decimal()
    {
        // Act
        var result = Result<decimal>.SuccessValidation();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(default(decimal), result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Failure_Should_Create_Failed_Result_With_Error_Message()
    {
        // Arrange
        string errorMessage = "Validation failed";

        // Act
        var result = Result<CdbCalculationResult>.Failure(errorMessage);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Equal(errorMessage, result.Error);
    }

    [Fact]
    public void Failure_Should_Create_Failed_Result_With_Error_Message_For_Value_Type()
    {
        // Arrange
        string errorMessage = "Invalid value";

        // Act
        var result = Result<int>.Failure(errorMessage);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(default(int), result.Value);
        Assert.Equal(errorMessage, result.Error);
    }

    [Fact]
    public void Failure_Should_Create_Failed_Result_With_Null_Error()
    {
        // Arrange
        string? errorMessage = null;

        // Act
        var result = Result<CdbCalculationResult>.Failure(errorMessage!);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Failure_Should_Create_Failed_Result_With_Empty_Error_Message()
    {
        // Arrange
        string errorMessage = string.Empty;

        // Act
        var result = Result<CdbCalculationResult>.Failure(errorMessage);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Equal(string.Empty, result.Error);
    }

    [Fact]
    public void Result_Should_Support_Equality_Comparison_For_Success()
    {
        // Arrange
        var value = new CdbCalculationResult(1000.00m, 900.00m);
        var result1 = Result<CdbCalculationResult>.Success(value);
        var result2 = Result<CdbCalculationResult>.Success(value);

        // Act & Assert
        Assert.Equal(result1, result2);
        Assert.True(result1 == result2);
        Assert.False(result1 != result2);
    }

    [Fact]
    public void Result_Should_Support_Equality_Comparison_For_Failure()
    {
        // Arrange
        string errorMessage = "Error occurred";
        var result1 = Result<CdbCalculationResult>.Failure(errorMessage);
        var result2 = Result<CdbCalculationResult>.Failure(errorMessage);

        // Act & Assert
        Assert.Equal(result1, result2);
        Assert.True(result1 == result2);
        Assert.False(result1 != result2);
    }

    [Fact]
    public void Result_Should_Support_Equality_Comparison_For_SuccessValidation()
    {
        // Arrange
        var result1 = Result<CdbCalculationResult>.SuccessValidation();
        var result2 = Result<CdbCalculationResult>.SuccessValidation();

        // Act & Assert
        Assert.Equal(result1, result2);
        Assert.True(result1 == result2);
    }

    [Fact]
    public void Result_Should_Not_Be_Equal_When_Values_Differ()
    {
        // Arrange
        var value1 = new CdbCalculationResult(1000.00m, 900.00m);
        var value2 = new CdbCalculationResult(2000.00m, 1800.00m);
        var result1 = Result<CdbCalculationResult>.Success(value1);
        var result2 = Result<CdbCalculationResult>.Success(value2);

        // Act & Assert
        Assert.NotEqual(result1, result2);
        Assert.False(result1 == result2);
        Assert.True(result1 != result2);
    }

    [Fact]
    public void Result_Should_Not_Be_Equal_When_One_Is_Success_And_Other_Is_Failure()
    {
        // Arrange
        var value = new CdbCalculationResult(1000.00m, 900.00m);
        var successResult = Result<CdbCalculationResult>.Success(value);
        var failureResult = Result<CdbCalculationResult>.Failure("Error");

        // Act & Assert
        Assert.NotEqual(successResult, failureResult);
        Assert.False(successResult == failureResult);
        Assert.True(successResult != failureResult);
    }

    [Fact]
    public void Result_Should_Not_Be_Equal_When_Error_Messages_Differ()
    {
        // Arrange
        var result1 = Result<CdbCalculationResult>.Failure("Error 1");
        var result2 = Result<CdbCalculationResult>.Failure("Error 2");

        // Act & Assert
        Assert.NotEqual(result1, result2);
        Assert.False(result1 == result2);
    }


 

    [Fact]
    public void Result_Should_Have_Correct_ToString_For_Success()
    {
        // Arrange
        var value = new CdbCalculationResult(1000.00m, 900.00m);
        var result = Result<CdbCalculationResult>.Success(value);

        // Act
        var toString = result.ToString();

        // Assert
        Assert.NotNull(toString);
        Assert.Contains("IsSuccess = True", toString);
        Assert.Contains("Value", toString);
    }

    [Fact]
    public void Result_Should_Have_Correct_ToString_For_Failure()
    {
        // Arrange
        string errorMessage = "Validation failed";
        var result = Result<CdbCalculationResult>.Failure(errorMessage);

        // Act
        var toString = result.ToString();

        // Assert
        Assert.NotNull(toString);
        Assert.Contains("IsSuccess = False", toString);
        Assert.Contains(errorMessage, toString);
    }

    [Theory]
    [InlineData("Error message 1")]
    [InlineData("Error message 2")]
    [InlineData("")]
    public void Failure_Should_Accept_Different_Error_Messages(string errorMessage)
    {
        // Act
        var result = Result<CdbCalculationResult>.Failure(errorMessage);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(errorMessage, result.Error);
    }

    [Fact]
    public void Success_Should_Work_With_Complex_Objects()
    {
        // Arrange
        var complexValue = new CdbCalculationResult(1234.56m, 987.65m);

        // Act
        var result = Result<CdbCalculationResult>.Success(complexValue);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(1234.56m, result.Value.GrossValue);
        Assert.Equal(987.65m, result.Value.NetWorth);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Result_Should_Be_Immutable()
    {
        // Arrange
        var value1 = new CdbCalculationResult(1000.00m, 900.00m);
        var value2 = new CdbCalculationResult(2000.00m, 1800.00m);
        var result = Result<CdbCalculationResult>.Success(value1);
        
        var newResult = Result<CdbCalculationResult>.Success(value2);


        Assert.Equal(value1, result.Value);
        Assert.NotEqual(result, newResult);
    }

    [Fact]
    public void SuccessValidation_Should_Return_Same_Instance_Behavior_For_Value_Types()
    {
        // Act
        var result1 = Result<int>.SuccessValidation();
        var result2 = Result<int>.SuccessValidation();

        // Assert        
        Assert.Equal(result1, result2);
        Assert.True(result1.IsSuccess);
        Assert.True(result2.IsSuccess);
        Assert.Equal(default(int), result1.Value);
        Assert.Equal(default(int), result2.Value);
    }

    [Fact]
    public void Result_Should_Handle_Null_Value_In_Success_For_Nullable_Types()
    {
        // Arrange
        CdbCalculationResult? nullValue = null;

        // Act
        var result = Result<CdbCalculationResult?>.Success(nullValue!);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Result_Should_Distinguish_Between_Success_And_SuccessValidation()
    {
        // Arrange
        var value = new CdbCalculationResult(1000.00m, 900.00m);
        var successResult = Result<CdbCalculationResult>.Success(value);
        var successValidationResult = Result<CdbCalculationResult>.SuccessValidation();

        // Assert
        Assert.True(successResult.IsSuccess);
        Assert.True(successValidationResult.IsSuccess);
        Assert.NotNull(successResult.Value);
        Assert.Null(successValidationResult.Value);
        Assert.NotEqual(successResult, successValidationResult);
    }
}

