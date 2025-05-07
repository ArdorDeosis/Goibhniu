namespace Goibhniu.Shared.Tests;

public class ResultTests
{
  private const int SuccessValue = 0xC0FFEE;
  private const string ErrorMessage = "oh boy";
  
  [Test]
  public void Success_CreatesSuccessResult()
  {
    // Arrange & Act
    var result = Result<int>.Success(SuccessValue);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.IsError, Is.False);
      Assert.That(result.Data, Is.EqualTo(SuccessValue));
    });
  }

  [Test]
  public void Error_CreatesErrorResult()
  {
    // Arrange & Act
    var result = Result<int>.Error(ErrorMessage);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.False);
      Assert.That(result.IsError, Is.True);
      Assert.That(result.ErrorMessage, Is.EqualTo(ErrorMessage));
    });
  }

  [Test]
  public void ImplicitConversion_FromValue_CreatesSuccessResult()
  {
    // Arrange & Act
    Result<int> result = SuccessValue;

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.IsError, Is.False);
      Assert.That(result.Data, Is.EqualTo(SuccessValue));
    });
  }

  [Test]
  public void ImplicitConversion_FromErrorMessage_CreatesErrorResult()
  {
    // Arrange & Act
    Result<int> result = ErrorMessage;

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.False);
      Assert.That(result.IsError, Is.True);
      Assert.That(result.ErrorMessage, Is.EqualTo(ErrorMessage));
    });
  }

  [Test]
  public void ToSuccess_Extension_CreatesSuccessResult()
  {
    // Arrange & Act
    var result = SuccessValue.ToSuccess();

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.IsError, Is.False);
      Assert.That(result.Data, Is.EqualTo(SuccessValue));
    });
  }

  [Test]
  public void Success_WithComplexObjectType_StoresReferenceCorrectly()
  {
    // Arrange
    var value = new {};

    // Act
    var result = Result<object>.Success(value);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.Data, Is.SameAs(value));
    });
  }
}