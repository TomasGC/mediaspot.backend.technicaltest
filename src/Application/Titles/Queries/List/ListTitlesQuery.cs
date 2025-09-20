using Mediaspot.Domain.Titles;
using Mediaspot.Domain.Titles.Enums;
using MediatR;

namespace Mediaspot.Application.Titles.Queries.List;

public sealed record ListTitlesQuery(
    TitleType? Type = null,
    DateTime? FromReleaseDate = null,
    DateTime? ToReleaseDate = null,
    string? NamePattern = null,
    string? OriginCountry = null,
    string? OriginalLanguage = null,
    ushort? Page = 0,
    ushort? PageSize = 10) : IRequest<List<Title>>;
