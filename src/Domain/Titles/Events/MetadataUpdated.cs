using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Titles.Events;

public sealed record MetadataUpdated : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOnUtc { get; }

    public MetadataUpdated(Guid titleId)
    {
        Id = titleId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}
