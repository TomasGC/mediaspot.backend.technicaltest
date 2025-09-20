using Mediaspot.Domain.Common;
using MediatR;

namespace Mediaspot.Domain.Titles.Events;

public sealed record MetadataUpdated(Guid TitleId) : IDomainEvent, INotification
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}
