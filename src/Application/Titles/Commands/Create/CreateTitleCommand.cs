using Mediaspot.Domain.Titles.Enums;
using Mediaspot.Domain.Titles.ValueObjects;
using MediatR;

namespace Mediaspot.Application.Titles.Commands.Create;

public sealed record CreateTitleCommand(
    string ExternalId,
    TitleType Type,
    string Name,
    Origin Origin,
    string? Description,
    ushort? SeasonNumber) : IRequest<Guid>;
