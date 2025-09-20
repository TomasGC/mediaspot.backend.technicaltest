using Mediaspot.Application.Titles.Commands.Create;
using Mediaspot.Application.Titles.Commands.UpdateMetadata;
using Shouldly;

namespace Mediaspot.UnitTests.Titles.Commands.Create;

public class UpdateTitleMetadataValidatorTests
{
    private readonly UpdateTitleMetadataValidator _validator = new();

    [Fact]
    public void Validate_Should_Validate()
    {
        // Arrange
        var cmd = new UpdateTitleMetadataCommand(Guid.NewGuid(), "name", "France", "French", "description", null);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_Should_Throw_When_Name_Is_Null_Or_Empty(string name)
    {
        // Arrange
        var cmd = new UpdateTitleMetadataCommand(Guid.NewGuid(), name, "France", "French", "description", null);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_Should_Throw_When_Origin_Country_Is_Null_Or_Empty(string originCountry)
    {
        // Arrange
        var cmd = new UpdateTitleMetadataCommand(Guid.NewGuid(), "name", originCountry, "French", "description", null);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_Should_Throw_When_Original_Language_Is_Null_Or_Empty(string originalLanguage)
    {
        // Arrange
        var cmd = new UpdateTitleMetadataCommand(Guid.NewGuid(), "name", "France", originalLanguage, "description", null);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }
}
