using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Transcoding.Events;

public sealed record TranscodeJobStarted : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOnUtc { get; }

    public TranscodeJobStarted(Guid transcodeJobId)
    {
        Id = transcodeJobId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}
