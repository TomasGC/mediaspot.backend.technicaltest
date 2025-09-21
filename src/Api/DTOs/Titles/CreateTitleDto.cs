using Mediaspot.Domain.Titles.Enums;

namespace Mediaspot.Api.DTOs.Titles;

public sealed record CreateTitleDto(string ExternalId,
    TitleType Type,
    string Name,
    string OriginCountry,
    string OriginalLanguage,
    string? Description,
    ushort? SeasonNumber);
