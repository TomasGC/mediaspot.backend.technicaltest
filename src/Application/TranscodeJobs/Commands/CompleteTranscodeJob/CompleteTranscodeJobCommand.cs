using MediatR;

namespace Mediaspot.Application.TranscodeJobs.Commands.CompleteTranscodeJob;

public sealed record CompleteTranscodeJobCommand(Guid TranscodeJobId) : IRequest<Guid>;
