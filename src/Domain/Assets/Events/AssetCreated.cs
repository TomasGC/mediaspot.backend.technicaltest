using Mediaspot.Domain.Assets.Enums;
using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Assets.Events;

public sealed record AssetCreated : IDomainEvent
{
    public Guid Id { get; }
    public AssetType Type { get; }
    public DateTime OccurredOnUtc { get; }

    public AssetCreated(Guid assetId, AssetType type)
    {
        Id = assetId;
        Type = type;
        OccurredOnUtc = DateTime.UtcNow;
    }
}
