using Mediaspot.Application.Common;
using Mediaspot.Domain.Assets.ValueObjects;
using MediatR;

namespace Mediaspot.Application.Assets.Commands.UpdateMetadata;

public sealed class UpdateAssetMetadataHandler(IAssetRepository repo, IUnitOfWork uow)
    : IRequestHandler<UpdateAssetMetadataCommand>
{
    public async Task Handle(UpdateAssetMetadataCommand request, CancellationToken ct)
    {
        var asset = await repo.GetAsync(request.AssetId, ct) ?? throw new KeyNotFoundException("Asset not found");

        asset.UpdateMetadata(new Metadata(request.Title, request.Description, request.Language));

        await uow.SaveChangesAsync(ct);
    }
}
