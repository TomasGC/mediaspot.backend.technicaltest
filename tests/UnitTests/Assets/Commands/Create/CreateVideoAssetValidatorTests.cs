using FluentValidation;
using Mediaspot.Application.Assets.Commands.Create;
using Shouldly;

namespace Mediaspot.UnitTests.Assets.Commands.Create;

public class CreateVideoAssetValidatorTests : BaseCreateAssetValidatorTests<CreateVideoAssetCommand, CreateVideoAssetValidator>
{
    [Fact]
    public void Validate_Should_Throw_When_Duration_Is_0()
    {
        // Arrange
        var cmd = new CreateVideoAssetCommand("ext-unique", "title", "description", "en", 0, "4k", 24, "H.263");

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_Should_Throw_When_Resolution_Is_Null_Or_Empty(string resolution)
    {
        // Arrange
        var cmd = new CreateVideoAssetCommand("ext-unique", "title", "description", "en", 1000, resolution, 24, "H.263");

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Fact]
    public void Validate_Should_Throw_When_FrameRate_Is_0()
    {
        // Arrange
        var cmd = new CreateVideoAssetCommand("ext-unique", "title", "description", "en", 1000, "4k", 0, "H.263");

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_Should_Throw_When_Codec_Is_Null_Or_Empty(string codec)
    {
        // Arrange
        var cmd = new CreateVideoAssetCommand("ext-unique", "title", "description", "en", 1000, "4k", 24, codec);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    protected override CreateVideoAssetCommand GetCommandObject(string externalId, string title)
    {
        return new(externalId, title, "description", "en", 1000, "4k", 24, "H.263");
    }
}
