using Mediaspot.Application.Common;
using Mediaspot.Domain.Titles;
using Mediaspot.Domain.Titles.Filters;
using Microsoft.EntityFrameworkCore;

namespace Mediaspot.Infrastructure.Persistence;

public sealed class TitleRepository(MediaspotDbContext db) : ITitleRepository
{

    public Task<List<Title>> ListAsync(ListTitleQueryFilters filters, ushort page, ushort pageSize, CancellationToken ct)
    {
        var query = db.Titles.AsQueryable();

        if (filters.Type.HasValue)
        {
            query = query.Where(a => filters.Type.Value == a.Type);
        }

        if (filters.FromDate.HasValue)
        {
            query = query.Where(a => filters.FromDate.Value <= a.ReleaseDate);
        }

        if (filters.ToDate.HasValue)
        {
            query = query.Where(a => filters.ToDate.Value >= a.ReleaseDate);
        }

        if (!string.IsNullOrWhiteSpace(filters.NamePattern))
        {
            query = query.Where(a => a.Metadata.Name.Contains(filters.NamePattern, StringComparison.InvariantCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filters.OriginCountry))
        {
            query = query.Where(a => string.Equals(filters.OriginCountry, a.Metadata.Origin.Country, StringComparison.InvariantCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filters.OriginalLanguage))
        {
            query = query.Where(a => string.Equals(filters.OriginalLanguage, a.Metadata.Origin.Language, StringComparison.InvariantCultureIgnoreCase));
        }

        return query.Skip(page * pageSize).Take(pageSize).ToListAsync(ct);
    }

    public Task<Title?> GetAsync(Guid id, CancellationToken ct)
        => db.Titles.FirstOrDefaultAsync(a => a.Id == id, ct);

    public Task<Title?> GetByExternalIdAsync(string externalId, CancellationToken ct)
        => db.Titles.FirstOrDefaultAsync(a => a.ExternalId == externalId, ct);

    public async Task AddAsync(Title title, CancellationToken ct) => await db.Titles.AddAsync(title, ct);
}
