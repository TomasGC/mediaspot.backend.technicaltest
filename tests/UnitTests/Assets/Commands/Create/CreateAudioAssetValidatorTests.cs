using Mediaspot.Application.Assets.Commands.Create;
using Mediaspot.UnitTests.Assets.Commands.Create;
using Shouldly;

namespace Mediaspot.UnitTests.Titles.Commands.Create;

public class CreateAudioAssetValidatorTests : BaseCreateAssetValidatorTests<CreateAudioAssetCommand, CreateAudioAssetValidator>
{
    [Fact]
    public void Validate_Should_Throw_When_Duration_Is_0()
    {
        // Arrange
        var cmd = new CreateAudioAssetCommand("ext-unique", "title", "description", "en", 0, 320, 48000, "7.1");

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Fact]
    public void Validate_Should_Throw_When_Bitrate_Is_0()
    {
        // Arrange
        var cmd = new CreateAudioAssetCommand("ext-unique", "title", "description", "en", 1000, 0, 48000, "7.1");

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Fact]
    public void Validate_Should_Throw_When_SampleRate_Is_0()
    {
        // Arrange
        var cmd = new CreateAudioAssetCommand("ext-unique", "title", "description", "en", 1000, 320, 0, "7.1");

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_Should_Throw_When_Channels_Is_Null_Or_Empty(string channels)
    {
        // Arrange
        var cmd = new CreateAudioAssetCommand("ext-unique", "title", "description", "en", 1000, 320, 48000, channels);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    protected override CreateAudioAssetCommand GetCommandObject(string externalId, string title)
    {
        return new(externalId, title, "description", "en", 1000, 320, 48000, "7.1");
    }
}
