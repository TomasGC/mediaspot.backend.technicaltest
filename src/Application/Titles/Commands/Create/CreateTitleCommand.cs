using Mediaspot.Domain.Titles.Enums;
using MediatR;

namespace Mediaspot.Application.Titles.Commands.Create;

public sealed record CreateTitleCommand(
    string ExternalId,
    TitleType Type,
    string Name,
    string OriginCountry,
    string OriginalLanguage,
    string? Description,
    ushort? SeasonNumber) : IRequest<Guid>;
