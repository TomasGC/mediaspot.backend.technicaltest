using Mediaspot.Application.Common;
using Mediaspot.Application.Titles.Commands.Create;
using Mediaspot.Domain.Titles;
using Mediaspot.Domain.Titles.Enums;
using Moq;
using Shouldly;

namespace Mediaspot.UnitTests.Titles.Commands;

public class CreateTitleHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Title_When_ExternalId_Is_Unique()
    {
        // Arrange
        var repo = new Mock<ITitleRepository>();
        var uow = new Mock<IUnitOfWork>();

        repo.Setup(r => r.GetByExternalIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((Title?)null);
        repo.Setup(r => r.AddAsync(It.IsAny<Title>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateTitleHandler(repo.Object, uow.Object);
        var cmd = new CreateTitleCommand("ext-unique", TitleType.Movie, "name", "France", "French", "desc", null);

        // Act
        var id = await handler.Handle(cmd, CancellationToken.None);

        // Assert
        id.ShouldNotBe(Guid.Empty);
        repo.Verify(r => r.AddAsync(It.IsAny<Title>(), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_ExternalId_Exists()
    {
        // Arrange
        var repo = new Mock<ITitleRepository>();
        var uow = new Mock<IUnitOfWork>();
        var title = new Title("ext-unique", TitleType.Movie, new("name", new("France", "French"), "desc", null));

        repo.Setup(r => r.GetByExternalIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(title);

        var handler = new CreateTitleHandler(repo.Object, uow.Object);
        var cmd = new CreateTitleCommand("ext-unique", TitleType.Movie, "name", "France", "French", "desc", null);

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(() => handler.Handle(cmd, CancellationToken.None));
    }
}
