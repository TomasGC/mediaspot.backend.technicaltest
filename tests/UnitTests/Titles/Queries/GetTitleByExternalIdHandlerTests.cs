using Mediaspot.Application.Common;
using Mediaspot.Application.Titles.Queries.GetByExternalId;
using Mediaspot.Domain.Titles;
using Mediaspot.Domain.Titles.Enums;
using Moq;
using Shouldly;

namespace Mediaspot.UnitTests.Titles.Queries;

public class GetTitleByExternalIdHandlerTests
{
    [Fact]
    public async Task Handle_Should_Get_Title_By_External_Id()
    {
        // Arrange
        var title = new Title("ext-1", TitleType.Movie, new("name", new("France", "French"), "description", null));

        var repo = new Mock<ITitleRepository>();
        repo.Setup(r => r.GetByExternalIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(title);

        var handler = new GetTitleByExternalIdHandler(repo.Object);
        var query = new GetTitleByExternalIdQuery(title.ExternalId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Id.ShouldBe(title.Id);
        result.ExternalId.ShouldBe(title.ExternalId);
        result.Type.ShouldBe(title.Type);
        result.Metadata.ShouldBe(title.Metadata);
    }

    [Fact]
    public async Task Handle_Should_Throw_If_Title_Not_Found()
    {
        // Arrange
        var repo = new Mock<ITitleRepository>();
        repo.Setup(r => r.GetByExternalIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((Title?)null);

        var handler = new GetTitleByExternalIdHandler(repo.Object);
        var query = new GetTitleByExternalIdQuery("ext-1");

        // Act & Assert
        await Should.ThrowAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}
