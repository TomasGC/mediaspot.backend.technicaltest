using MediatR;

namespace Mediaspot.Application.Assets.Commands.Create;

public sealed record CreateAssetCommand(string ExternalId, string Title, string? Description, string? Language) : IRequest<Guid>;
