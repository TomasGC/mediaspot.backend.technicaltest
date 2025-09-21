using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Assets.Events;

public sealed record TranscodeRequested : IDomainEvent
{
    public Guid Id { get; }
    public Guid AssetId { get; }
    public string TargetPreset { get; }
    public DateTime OccurredOnUtc { get; }

    public TranscodeRequested(Guid assetId, Guid mediaFileId, string targetPreset)
    {
        Id = mediaFileId;
        AssetId = assetId;
        TargetPreset = targetPreset;
        OccurredOnUtc = DateTime.UtcNow;
    }
}