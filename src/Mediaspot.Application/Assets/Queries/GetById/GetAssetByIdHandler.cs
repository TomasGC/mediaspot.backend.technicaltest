using Mediaspot.Application.Common;
using Mediaspot.Domain.Assets;
using MediatR;

namespace Mediaspot.Application.Assets.Queries.GetById;

public sealed class GetAssetByIdHandler(IAssetRepository repo) : IRequestHandler<GetAssetByIdQuery, Asset>
{
    public async Task<Asset> Handle(GetAssetByIdQuery request, CancellationToken ct)
    {
        return await repo.GetAsync(request.AssetId, ct) ?? throw new KeyNotFoundException("Asset not found");
    }
}

