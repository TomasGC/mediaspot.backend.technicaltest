using Mediaspot.Application.Common;
using Mediaspot.Domain.Assets;
using Microsoft.EntityFrameworkCore;

namespace Mediaspot.Infrastructure.Persistence;
public sealed class AssetRepository(MediaspotDbContext db) : IAssetRepository
{
    public Task<BaseAsset?> GetAsync(Guid id, CancellationToken ct)
        => db.Assets.Include("_mediaFiles").FirstOrDefaultAsync(a => a.Id == id, ct);

    public Task<BaseAsset?> GetByExternalIdAsync(string externalId, CancellationToken ct)
        => db.Assets.FirstOrDefaultAsync(a => a.ExternalId == externalId, ct);

    public async Task AddAsync(BaseAsset asset, CancellationToken ct) => await db.Assets.AddAsync(asset, ct);
}
