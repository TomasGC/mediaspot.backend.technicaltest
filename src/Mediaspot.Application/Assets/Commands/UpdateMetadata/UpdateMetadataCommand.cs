using MediatR;

namespace Mediaspot.Application.Assets.Commands.UpdateMetadata;

public sealed record UpdateMetadataCommand(Guid AssetId, string Title, string? Description, string? Language) : IRequest;
