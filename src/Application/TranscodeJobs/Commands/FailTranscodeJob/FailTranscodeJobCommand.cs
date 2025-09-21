using MediatR;

namespace Mediaspot.Application.TranscodeJobs.Commands.FailTranscodeJob;

public sealed record FailTranscodeJobCommand(Guid TranscodeJobId) : IRequest<Guid>;
