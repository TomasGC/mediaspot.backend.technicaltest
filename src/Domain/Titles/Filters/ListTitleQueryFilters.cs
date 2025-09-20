using Mediaspot.Domain.Titles.Enums;

namespace Mediaspot.Domain.Titles.Filters
{
    public sealed record ListTitleQueryFilters(
        TitleType? Type,
        string? NamePattern,
        DateTime? FromDate,
        DateTime? ToDate,
        string? OriginCountry,
        string? OriginalLanguage);
}
