using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Assets.Events;

public sealed record AssetArchived(Guid AssetId) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}