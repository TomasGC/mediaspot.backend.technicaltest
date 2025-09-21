using Mediaspot.Domain.Transcoding;
using Mediaspot.Domain.Transcoding.Events;
using Shouldly;

namespace Mediaspot.UnitTests.TranscodeJobs;

public class TranscodeJobTests
{
    [Fact]
    public void Constructor_Should_Set_Properties_And_Raise_TranscodeJobCreated()
    {
        // Arrange
        var assetId = Guid.NewGuid();
        var mediaFileId = Guid.NewGuid();
        var preset = "4k";

        // Act
        var transcodeJob = new TranscodeJob(assetId, mediaFileId, preset);

        // Assert
        transcodeJob.AssetId.ShouldBe(assetId);
        transcodeJob.MediaFileId.ShouldBe(mediaFileId);
        transcodeJob.Preset.ShouldBe(preset);
        transcodeJob.Status.ShouldBe(TranscodeStatus.Pending);
        transcodeJob.DomainEvents.OfType<TranscodeJobCreated>().Any(ac => ac.TranscodeId == transcodeJob.Id).ShouldBeTrue();
    }
}
