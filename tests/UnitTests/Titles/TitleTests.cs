using Mediaspot.Domain.Titles;
using Mediaspot.Domain.Titles.Enums;
using Mediaspot.Domain.Titles.Events;
using Mediaspot.Domain.Titles.ValueObjects;
using Shouldly;

namespace Mediaspot.UnitTests.Titles;

public class TitleTests
{
    [Fact]
    public void Constructor_Should_Set_Properties_And_Raise_TitleCreated()
    {
        // Arrange
        var externalId = "ext-1";
        var type = TitleType.Movie;
        var origin = new Origin("France", "French");
        var metadata = new Metadata("name", origin, "description", null);

        // Act
        var title = new Title(externalId, type, metadata);

        // Assert
        title.ExternalId.ShouldBe(externalId);
        title.Type.ShouldBe(type);
        title.Metadata.ShouldBe(metadata);
        title.DomainEvents.OfType<TitleCreated>().Any(ac => ac.TitleId == title.Id).ShouldBeTrue();
    }
}
