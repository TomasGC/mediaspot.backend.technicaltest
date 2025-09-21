using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Transcoding.Events;

public sealed record TranscodeJobCreated : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOnUtc { get; }

    public TranscodeJobCreated(Guid transcodeId)
    {
        Id = transcodeId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}
