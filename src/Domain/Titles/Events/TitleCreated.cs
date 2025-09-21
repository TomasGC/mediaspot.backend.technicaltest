using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Titles.Events;

public sealed record TitleCreated : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOnUtc { get; }

    public TitleCreated(Guid titleId)
    {
        Id = titleId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}
