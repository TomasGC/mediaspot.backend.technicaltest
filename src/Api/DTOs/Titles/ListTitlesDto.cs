using Mediaspot.Domain.Titles.Enums;

namespace Mediaspot.Api.DTOs.Titles;

public sealed record ListTitlesDto(
    TitleType? Type = null,
DateTime? FromReleaseDate = null,
DateTime? ToReleaseDate = null,
string? NamePattern = null,
string? OriginCountry = null,
string? OriginalLanguage = null,
ushort? Page = 0,
ushort? PageSize = 10)
{
}
