using Mediaspot.Application.Common;
using Mediaspot.Application.Titles.Commands.Create;
using Mediaspot.Application.Titles.Commands.UpdateMetadata;
using Mediaspot.Domain.Titles;
using Mediaspot.Domain.Titles.Enums;
using Moq;
using Shouldly;

namespace Mediaspot.UnitTests.Titles.Commands.UpdateMetadata;

public class UpdateTitleMetadataHandlerTests
{
    [Fact]
    public async Task Handle_Should_Update_Metadata_And_Save()
    {
        // Arrange
        var title = new Title("ext-unique", TitleType.Movie, new("name", new("France", "French"), "description", null));
        var repo = new Mock<ITitleRepository>();
        var uow = new Mock<IUnitOfWork>();

        repo.Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(title);
        uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new UpdateTitleMetadataHandler(repo.Object, uow.Object);
        var cmd = new UpdateTitleMetadataCommand(title.Id, "new", "France", "French", "description", null);

        // Act
        await handler.Handle(cmd, CancellationToken.None);

        // Assert
        title.Metadata.Name.ShouldBe(cmd.Name);
        repo.Verify(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineData((ushort)0)]
    [InlineData((ushort)1)]
    public async Task Handle_Should_Throw_When_Type_Is_Not_TvShow_And_SeasonNumber_Is_Not_Null(ushort seasonNumber)
    {
        // Arrange
        var repo = new Mock<ITitleRepository>();
        var uow = new Mock<IUnitOfWork>();
        var title = new Title("ext-unique", TitleType.Movie, new("name", new("France", "French"), "description", null));

        repo.Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(title);

        var handler = new UpdateTitleMetadataHandler(repo.Object, uow.Object);
        var cmd = new UpdateTitleMetadataCommand(
            title.Id,
            title.Metadata.Name,
            title.Metadata.Origin.Country,
            title.Metadata.Origin.Language,
            title.Metadata.Description,
            seasonNumber);

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(() => handler.Handle(cmd, CancellationToken.None));
        repo.Verify(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
