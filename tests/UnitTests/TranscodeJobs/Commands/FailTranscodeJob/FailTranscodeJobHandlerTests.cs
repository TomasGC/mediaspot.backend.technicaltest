using Mediaspot.Application.Common;
using Mediaspot.Application.TranscodeJobs.Commands.FailTranscodeJob;
using Mediaspot.Domain.Transcoding;
using Moq;
using Shouldly;

namespace Mediaspot.UnitTests.TranscodeJobs.Commands.FailTranscodeJob;

public class FaildTranscodeJobHandlerTests
{
    [Fact]
    public async Task Handle_Should_Start_TranscodeJob_When_Exists()
    {
        // Arrange
        var repo = new Mock<ITranscodeJobRepository>();
        var uow = new Mock<IUnitOfWork>();
        var transcodeJob = new TranscodeJob(Guid.NewGuid(), Guid.NewGuid(), "4k");

        repo.Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transcodeJob);
        uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new FailTranscodeJobHandler(repo.Object, uow.Object);
        var cmd = new FailTranscodeJobCommand(transcodeJob.Id);

        // Act
        var id = await handler.Handle(cmd, CancellationToken.None);

        // Assert
        id.ShouldNotBe(Guid.Empty);
        transcodeJob.Status.ShouldBe(TranscodeStatus.Failed);
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

        var handler = new FailTranscodeJobHandler(repo.Object, uow.Object);
        var cmd = new FailTranscodeJobCommand(Guid.NewGuid());

        // Act & Assert
        await Should.ThrowAsync<KeyNotFoundException>(() => handler.Handle(cmd, CancellationToken.None));
        repo.Verify(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
