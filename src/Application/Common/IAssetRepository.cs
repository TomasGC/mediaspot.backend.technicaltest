using Mediaspot.Domain.Assets;

namespace Mediaspot.Application.Common;

public interface IAssetRepository
{
    Task<BaseAsset?> GetAsync(Guid id, CancellationToken ct);
    Task<BaseAsset?> GetByExternalIdAsync(string externalId, CancellationToken ct);
    Task AddAsync(BaseAsset asset, CancellationToken ct);
}
