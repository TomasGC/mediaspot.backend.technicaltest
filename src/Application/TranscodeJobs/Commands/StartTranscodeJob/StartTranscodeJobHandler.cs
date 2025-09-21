using Mediaspot.Application.Common;
using Mediaspot.Domain.Transcoding;
using MediatR;

namespace Mediaspot.Application.TranscodeJobs.Commands.StartTranscodeJob;

public sealed class StartTranscodeJobHandler(ITranscodeJobRepository repo, IUnitOfWork uow)
    : IRequestHandler<StartTranscodeJobCommand, Guid>
{
    public async Task<Guid> Handle(StartTranscodeJobCommand request, CancellationToken ct)
    {
        var transcodeJob = await repo.GetAsync(request.TranscodeJobId, ct);
        if (transcodeJob == null)
        {
            throw new KeyNotFoundException("TranscodeJob not found.");
        }

        if (transcodeJob.Status != TranscodeStatus.Pending)
        {
            throw new InvalidOperationException($"TranscodeJob can't start as it's [{transcodeJob.Status}] and needs to be [{TranscodeStatus.Pending}].");
        }

        transcodeJob.MarkRunning();

        await uow.SaveChangesAsync(ct);

        return transcodeJob.Id;
    }
}
