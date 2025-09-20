using Mediaspot.Domain.Common;
using MediatR;

namespace Mediaspot.Domain.Assets.Events;

public sealed record MetadataUpdated(Guid AssetId) : IDomainEvent, INotification
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}
