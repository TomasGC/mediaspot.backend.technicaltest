using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Assets.Events;
public sealed record MediaFileRegistered : IDomainEvent
{
    public Guid Id { get; }
    public Guid AssetId { get; }
    public DateTime OccurredOnUtc { get; }

    public MediaFileRegistered(Guid assetId, Guid mediaFileId)
    {
        Id = mediaFileId;
        AssetId = assetId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}
