using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Assets.Events;

public sealed record TranscodeRequested(Guid AssetId, Guid MediaFileId, string TargetPreset) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}