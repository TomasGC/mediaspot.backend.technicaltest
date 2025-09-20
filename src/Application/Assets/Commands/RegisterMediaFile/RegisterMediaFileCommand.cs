using MediatR;

namespace Mediaspot.Application.Assets.Commands.RegisterMediaFile;

public sealed record RegisterMediaFileCommand(Guid AssetId, string Path, double DurationSeconds) : IRequest<Guid>;
