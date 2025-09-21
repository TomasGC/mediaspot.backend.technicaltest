using Mediaspot.Application.Common;
using Mediaspot.Application.TranscodeJobs.Commands.StartTranscodeJob;
using Mediaspot.Domain.Transcoding;
using Moq;
using Shouldly;

namespace Mediaspot.UnitTests.TranscodeJobs.Commands.StartTranscodeJob;

public class StartTranscodeJobHandlerTests
{
    [Fact]
    public async Task Handle_Should_Start_TranscodeJob_When_Exists_And_Pending()
    {
        // Arrange
        var repo = new Mock<ITranscodeJobRepository>();
        var uow = new Mock<IUnitOfWork>();
        var transcodeJob = new TranscodeJob(Guid.NewGuid(), Guid.NewGuid(), "4k");

        repo.Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transcodeJob);
        uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new StartTranscodeJobHandler(repo.Object, uow.Object);
        var cmd = new StartTranscodeJobCommand(transcodeJob.Id);

        // Act
        var id = await handler.Handle(cmd, CancellationToken.None);

        // Assert
        id.ShouldNotBe(Guid.Empty);
        transcodeJob.Status.ShouldBe(TranscodeStatus.Running);
        repo.Verify(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_TranscodeJob_Does_Not_Exist()
    {
        // Arrange
        var repo = new Mock<ITranscodeJobRepository>();
        var uow = new Mock<IUnitOfWork>();

        repo.Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((TranscodeJob?)null);

        var handler = new StartTranscodeJobHandler(repo.Object, uow.Object);
        var cmd = new StartTranscodeJobCommand(Guid.NewGuid());

        // Act & Assert
        await Should.ThrowAsync<KeyNotFoundException>(() => handler.Handle(cmd, CancellationToken.None));
        repo.Verify(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_TranscodeJob_Is_Not_Pending()
    {
        // Arrange
        var repo = new Mock<ITranscodeJobRepository>();
        var uow = new Mock<IUnitOfWork>();
        var transcodeJob = new TranscodeJob(Guid.NewGuid(), Guid.NewGuid(), "4k");
        transcodeJob.MarkRunning();

        repo.Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transcodeJob);
        uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new StartTranscodeJobHandler(repo.Object, uow.Object);
        var cmd = new StartTranscodeJobCommand(transcodeJob.Id);

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(() => handler.Handle(cmd, CancellationToken.None));
        repo.Verify(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
