using MediatR;

namespace Mediaspot.Application.Assets.Commands.Archive;

public sealed record ArchiveAssetCommand(Guid AssetId) : IRequest;
