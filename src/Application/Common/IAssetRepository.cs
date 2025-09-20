using Mediaspot.Domain.Assets;

namespace Mediaspot.Application.Common;

public interface IAssetRepository
{
    Task<Asset?> GetAsync(Guid id, CancellationToken ct);
    Task<Asset?> GetByExternalIdAsync(string externalId, CancellationToken ct);
    Task AddAsync(Asset asset, CancellationToken ct);
}
