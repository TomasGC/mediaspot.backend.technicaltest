using Mediaspot.Application.Common;
using Mediaspot.Application.Events;
using Mediaspot.Application.Workers;
using Mediaspot.Domain.Assets.Events;
using Mediaspot.Domain.Transcoding;
using Moq;

namespace Mediaspot.UnitTests.Events;

public class TranscodeRequestedHandlerTests
{
    [Fact]
    public async Task Handle_Should_Add_TranscodeJob_And_Save()
    {
        // Arrange
        var repo = new Mock<ITranscodeJobRepository>();
        var uow = new Mock<IUnitOfWork>();
        var queue = new Mock<TranscodeJobQueue>();

        repo.Setup(r => r.AddAsync(It.IsAny<TranscodeJob>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new TranscodeRequestedHandler(repo.Object, uow.Object, queue.Object);
        var evt = new TranscodeRequested(Guid.NewGuid(), Guid.NewGuid(), "preset");

        // Act
        await handler.Handle(evt, CancellationToken.None);

        // Assert
        repo.Verify(r => r.AddAsync(
            It.Is<TranscodeJob>(j => j.AssetId == evt.AssetId && j.MediaFileId == evt.Id && j.Preset == evt.TargetPreset),
            It.IsAny<CancellationToken>()),
            Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        queue.Verify(q => q.EnqueueAsync(It.IsAny<TranscodeJob>()), Times.Once);
    }
}
