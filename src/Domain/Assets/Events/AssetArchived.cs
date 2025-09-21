using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Assets.Events;

public sealed record AssetArchived : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOnUtc { get; }

    public AssetArchived(Guid assetId)
    {
        Id = assetId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}