using Mediaspot.Domain.Common;
using Mediaspot.Domain.Transcoding.Events;

namespace Mediaspot.Domain.Transcoding;

public enum TranscodeStatus { Pending, Running, Succeeded, Failed }

public sealed class TranscodeJob : AggregateRoot
{
    public Guid AssetId { get; private set; }
    public Guid MediaFileId { get; private set; }
    public string Preset { get; private set; }
    public TranscodeStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private TranscodeJob()
    {
        AssetId = Guid.Empty;
        MediaFileId = Guid.Empty;
        Preset = string.Empty;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public TranscodeJob(Guid assetId, Guid mediaFileId, string preset)
    {
        AssetId = assetId;
        MediaFileId = mediaFileId;
        Preset = preset;
        Status = TranscodeStatus.Pending;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;

        Raise(new TranscodeJobCreated(Id));
    }

    public void MarkRunning()
    {
        Status = TranscodeStatus.Running;
        UpdatedAt = DateTime.UtcNow;

        Raise(new TranscodeJobStarted(Id));
    }

    public void MarkSucceeded()
    {
        Status = TranscodeStatus.Succeeded;
        UpdatedAt = DateTime.UtcNow;

        Raise(new TranscodeJobCompleted(Id));
    }

    public void MarkFailed()
    {
        Status = TranscodeStatus.Failed;
        UpdatedAt = DateTime.UtcNow;

        Raise(new TranscodeJobFailed(Id));
    }
}