using Mediaspot.Application.Common;
using Mediaspot.Application.Titles.Queries.List;
using Mediaspot.Domain.Titles;
using Mediaspot.Domain.Titles.Enums;
using Mediaspot.Domain.Titles.Filters;
using Moq;
using Shouldly;

namespace Mediaspot.UnitTests.Titles.Queries.List;

public class ListTitlesHandlerTests
{
    [Fact]
    public async Task Handle_Should_Get_Titles_Without_Filters()
    {
        // Arrange
        var title = new Title("ext-1", TitleType.Movie, new("name", new("France", "French"), "description", null));

        var repo = new Mock<ITitleRepository>();
        repo.Setup(r => r.ListAsync(It.IsAny<ListTitleQueryFilters>(), It.IsAny<ushort>(), It.IsAny<ushort>(), It.IsAny<CancellationToken>())).ReturnsAsync([title]);

        var handler = new ListTitlesHandler(repo.Object);
        var query = new ListTitlesQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeEmpty();
        result.Single()!.ShouldBe(title);
        repo.Verify(r => r.ListAsync(It.IsAny<ListTitleQueryFilters>(), It.IsAny<ushort>(), It.IsAny<ushort>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Get_Titles_With_Filter()
    {
        // Arrange
        var title = new Title("ext-1", TitleType.Movie, new("name", new("France", "French"), "description", null));

        var repo = new Mock<ITitleRepository>();
        repo.Setup(r => r.ListAsync(It.IsAny<ListTitleQueryFilters>(), It.IsAny<ushort>(), It.IsAny<ushort>(), It.IsAny<CancellationToken>())).ReturnsAsync([title]);

        var handler = new ListTitlesHandler(repo.Object);
        var query = new ListTitlesQuery(Type: title.Type);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeEmpty();
        result.Single()!.ShouldBe(title);
        repo.Verify(r => r.ListAsync(It.IsAny<ListTitleQueryFilters>(), It.IsAny<ushort>(), It.IsAny<ushort>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Not_Get_Titles_With_Wrong_Filter()
    {
        // Arrange
        var title = new Title("ext-1", TitleType.Movie, new("name", new("France", "French"), "description", null));

        var repo = new Mock<ITitleRepository>();
        repo.Setup(r => r.ListAsync(It.IsAny<ListTitleQueryFilters>(), It.IsAny<ushort>(), It.IsAny<ushort>(), It.IsAny<CancellationToken>())).ReturnsAsync([]);

        var handler = new ListTitlesHandler(repo.Object);
        var query = new ListTitlesQuery(Type: TitleType.Media);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeEmpty();
        repo.Verify(r => r.ListAsync(It.IsAny<ListTitleQueryFilters>(), It.IsAny<ushort>(), It.IsAny<ushort>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
