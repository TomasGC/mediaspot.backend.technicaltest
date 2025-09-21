using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Assets.Events;

public sealed record AssetCreated : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOnUtc { get; }

    public AssetCreated(Guid assetId)
    {
        Id = assetId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}
