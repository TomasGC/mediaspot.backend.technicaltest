using Mediaspot.Application.Assets.Commands.Create;
using Mediaspot.Application.Common;
using Mediaspot.Domain.Assets;
using Mediaspot.Domain.Assets.ValueObjects;
using Moq;
using Shouldly;

namespace Mediaspot.UnitTests.Assets.Commands.Create;

public class CreateAssetHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Asset_When_ExternalId_Is_Unique()
    {
        var repo = new Mock<IAssetRepository>();
        var uow = new Mock<IUnitOfWork>();
        repo.Setup(r => r.GetByExternalIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((AudioAsset?)null);
        repo.Setup(r => r.AddAsync(It.IsAny<BaseAsset>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var handler = new CreateAudioAssetHandler(repo.Object, uow.Object);
        var cmd = new CreateAudioAssetCommand("ext-unique", "title", "desc", "en", 1000, 320, 48000, "7.1");

        var id = await handler.Handle(cmd, CancellationToken.None);

        id.ShouldNotBe(Guid.Empty);
        repo.Verify(r => r.AddAsync(It.IsAny<BaseAsset>(), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_ExternalId_Exists()
    {
        var repo = new Mock<IAssetRepository>();
        var uow = new Mock<IUnitOfWork>();
        var asset = new VideoAsset("ext-unique", new Metadata("t", null, null), 1000, "4k", 24, "H.263");

        repo.Setup<Task<BaseAsset>>(r => r.GetByExternalIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(asset);
        var handler = new CreateVideoAssetHandler(repo.Object, uow.Object);
        var cmd = new CreateVideoAssetCommand(asset.ExternalId, "title", "desc", "en", 600, "8k", (float)29.97, "MPEG-2");

        await Should.ThrowAsync<InvalidOperationException>(() => handler.Handle(cmd, CancellationToken.None));
    }
}
