using Mediaspot.Application.Titles.Queries.List;
using Shouldly;

namespace Mediaspot.UnitTests.Titles.Commands.Create;

public class ListTitlesValidatorTests
{
    private readonly ListTitlesValidator _validator = new();

    [Fact]
    public void Validate_Should_Validate_When_Query_Is_Empty()
    {
        // Arrange
        var query = new ListTitlesQuery();

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_Should_Throw_When_ToReleaseDate_Is_Lower_Than_FromReleaseDate()
    {
        // Arrange
        var cmd = new ListTitlesQuery(FromReleaseDate: DateTime.Now, ToReleaseDate: DateTime.Now.AddDays(-1));

        // Act
        var result = _validator.Validate(cmd);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }
}
