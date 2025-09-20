using Mediaspot.Application.Titles.Commands.Create;
using Mediaspot.Domain.Titles.Enums;
using Shouldly;

namespace Mediaspot.UnitTests.Titles.Commands.Create;

public class CreateTitleValidatorTests
{
    private readonly CreateTitleValidator _validator = new();

    [Fact]
    public void Validate_Should_Validate_Movie()
    {
        // Arrange
        var cmd = new CreateTitleCommand("ext-unique", TitleType.Movie, "name", "France", "French", "description", null);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_Should_Validate_TvShow()
    {
        // Arrange
        var cmd = new CreateTitleCommand("ext-unique", TitleType.TvShow, "name", "France", "French", "description", 1);

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
        var cmd = new CreateTitleCommand(externalId, TitleType.Movie, "name", "France", "French", "description", null);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_Should_Throw_When_Name_Is_Null_Or_Empty(string name)
    {
        // Arrange
        var cmd = new CreateTitleCommand("ext-unique", TitleType.Movie, name, "France", "French", "description", null);

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
        var cmd = new CreateTitleCommand("ext-unique", TitleType.Movie, "name", originCountry, "French", "description", null);

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
        var cmd = new CreateTitleCommand("ext-unique", TitleType.Movie, "name", "France", originalLanguage, "description", null);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Theory]
    [InlineData((ushort)0)]
    [InlineData((ushort)1)]
    public void Validate_Should_Throw_When_Type_Is_Not_TvShow_And_SeasonNumber_Is_Not_Null(ushort seasonNumber)
    {
        // Arrange
        var cmd = new CreateTitleCommand("ext-unique", TitleType.Movie, "name", "France", "French", "description", seasonNumber);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData((ushort)0)]
    public void Validate_Should_Throw_When_Type_Is_TvShow_But_SeasonNumber_Is_Null_Or_Zero(ushort? seasonNumber)
    {
        // Arrange
        var cmd = new CreateTitleCommand("ext-unique", TitleType.TvShow, "name", "France", "French", "description", seasonNumber);

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }
}
