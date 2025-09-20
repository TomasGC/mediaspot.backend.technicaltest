using Mediaspot.Application.Common;
using Mediaspot.Domain.Titles;
using MediatR;

namespace Mediaspot.Application.Titles.Queries.GetByExternalId;

public sealed class GetTitleByExternalIdHandler(ITitleRepository repo) : IRequestHandler<GetTitleByExternalIdQuery, Title>
{
    public async Task<Title> Handle(GetTitleByExternalIdQuery request, CancellationToken ct)
    {
        return await repo.GetByExternalIdAsync(request.TitleExternalId, ct) ?? throw new KeyNotFoundException("Title not found");
    }
}

