using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Assets.Events;

public sealed record MetadataUpdated : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOnUtc { get; }

    public MetadataUpdated(Guid assetId)
    {
        Id = assetId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}
