using Mediaspot.Application.Common;
using Mediaspot.Domain.Transcoding;
using MediatR;

namespace Mediaspot.Application.TranscodeJobs.Commands.CompleteTranscodeJob;

public sealed class CompleteTranscodeJobHandler(ITranscodeJobRepository repo, IUnitOfWork uow)
    : IRequestHandler<CompleteTranscodeJobCommand, Guid>
{
    public async Task<Guid> Handle(CompleteTranscodeJobCommand request, CancellationToken ct)
    {
        var transcodeJob = await repo.GetAsync(request.TranscodeJobId, ct);
        if (transcodeJob == null)
        {
            throw new KeyNotFoundException("TranscodeJob not found.");
        }

        if (transcodeJob.Status != TranscodeStatus.Running)
        {
            throw new InvalidOperationException($"TranscodeJob can't start as it's [{transcodeJob.Status}] and needs to be [{TranscodeStatus.Running}].");
        }

        transcodeJob.MarkSucceeded();

        await uow.SaveChangesAsync(ct);

        return transcodeJob.Id;
    }
}
