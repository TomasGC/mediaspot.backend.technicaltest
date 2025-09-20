namespace Mediaspot.Api.DTOs.Titles;

public sealed record UpdateTitleMetadataDto(string Name, string OriginCountry, string OriginalLanguage, string? Description, ushort? SeasonNumber);
