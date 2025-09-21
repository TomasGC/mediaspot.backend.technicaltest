using Mediaspot.Application.Common;
using MediatR;

namespace Mediaspot.Application.TranscodeJobs.Commands.FailTranscodeJob;

public sealed class FailTranscodeJobHandler(ITranscodeJobRepository repo, IUnitOfWork uow)
    : IRequestHandler<FailTranscodeJobCommand, Guid>
{
    public async Task<Guid> Handle(FailTranscodeJobCommand request, CancellationToken ct)
    {
        var transcodeJob = await repo.GetAsync(request.TranscodeJobId, ct);
        if (transcodeJob == null)
        {
            throw new KeyNotFoundException("TranscodeJob not found.");
        }

        transcodeJob.MarkFailed();

        await uow.SaveChangesAsync(ct);

        return transcodeJob.Id;
    }
}
