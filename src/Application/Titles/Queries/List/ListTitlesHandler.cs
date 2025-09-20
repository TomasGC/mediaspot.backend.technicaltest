using Mediaspot.Application.Common;
using Mediaspot.Domain.Titles;
using MediatR;

namespace Mediaspot.Application.Titles.Queries.List;

public sealed class ListTitlesHandler(ITitleRepository repo) : IRequestHandler<ListTitlesQuery, List<Title>>
{
    public async Task<List<Title>> Handle(ListTitlesQuery request, CancellationToken ct)
    {
        return await repo.ListAsync(
            new(
                request.Type,
                request.NamePattern,
                request.FromReleaseDate,
                request.ToReleaseDate,
                request.OriginCountry,
                request.OriginalLanguage),
            request.Page!.Value,
            request.PageSize!.Value,
            ct);
    }
}

