using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Transcoding.Events;

public sealed record TranscodeJobFailed : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOnUtc { get; }

    public TranscodeJobFailed(Guid transcodeJobId)
    {
        Id = transcodeJobId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}
