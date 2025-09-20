using Mediaspot.Application.Common;
using Mediaspot.Domain.Titles;
using Microsoft.EntityFrameworkCore;

namespace Mediaspot.Infrastructure.Persistence;

public sealed class TitleRepository(MediaspotDbContext db) : ITitleRepository
{
    public Task<Title?> GetAsync(Guid id, CancellationToken ct)
        => db.Titles.FirstOrDefaultAsync(a => a.Id == id, ct);

    public Task<Title?> GetByExternalIdAsync(string externalId, CancellationToken ct)
        => db.Titles.FirstOrDefaultAsync(a => a.ExternalId == externalId, ct);
}
