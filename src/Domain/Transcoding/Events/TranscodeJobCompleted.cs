using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Transcoding.Events;

public sealed record TranscodeJobCompleted : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOnUtc { get; }

    public TranscodeJobCompleted(Guid transcodeJobId)
    {
        Id = transcodeJobId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}
