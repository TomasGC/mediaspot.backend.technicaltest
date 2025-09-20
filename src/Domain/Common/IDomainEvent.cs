using MediatR;

namespace Mediaspot.Domain.Common;

public interface IDomainEvent : INotification
{
    DateTime OccurredOnUtc { get; }
}
