using MediatR;

namespace Mediaspot.Application.TranscodeJobs.Commands.StartTranscodeJob;

public sealed record StartTranscodeJobCommand(Guid TranscodeJobId) : IRequest<Guid>;
