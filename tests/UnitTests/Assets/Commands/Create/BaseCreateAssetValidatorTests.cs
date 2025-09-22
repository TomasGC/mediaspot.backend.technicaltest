using Mediaspot.Application.Assets.Commands.Create;
using Shouldly;

namespace Mediaspot.UnitTests.Assets.Commands.Create;

public abstract class BaseCreateAssetValidatorTests<TCommand, TValidator>
    where TCommand : BaseCreateAssetCommand
    where TValidator : BaseCreateAssetValidator<TCommand>, new()
{
    protected readonly TValidator _validator = new();

    [Fact]
    public void Validate_Should_Validate_Asset()
    {
        // Arrange
        var cmd = GetCommandObject("ext-1", "title");

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_Should_Throw_When_ExternalId_Is_Null_Or_Empty(string externalId)
    {
        // Arrange
        var cmd = GetCommandObject(externalId, "title");

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_Should_Throw_When_Title_Is_Null_Or_Empty(string title)
    {
        // Arrange
        var cmd = GetCommandObject("ext-1", title);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    protected abstract TCommand GetCommandObject(string externalId, string title);
}
