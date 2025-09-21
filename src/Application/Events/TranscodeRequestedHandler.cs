using Mediaspot.Application.Common;
using Mediaspot.Application.Workers;
using Mediaspot.Domain.Assets.Events;
using Mediaspot.Domain.Transcoding;
using MediatR;

namespace Mediaspot.Application.Events;

public sealed class TranscodeRequestedHandler(
    ITranscodeJobRepository repo,
    IUnitOfWork uow,
    TranscodeJobQueue queue)
    : INotificationHandler<TranscodeRequested>
{
    public async Task Handle(TranscodeRequested @event, CancellationToken ct)
    {
        var job = new TranscodeJob(@event.AssetId, @event.Id, @event.TargetPreset);
        await repo.AddAsync(job, ct);
        await uow.SaveChangesAsync(ct);

        await queue.EnqueueAsync(job);
    }
}
