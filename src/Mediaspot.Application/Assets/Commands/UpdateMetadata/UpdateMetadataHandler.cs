using Mediaspot.Application.Common;
using Mediaspot.Domain.Assets.ValueObjects;
using MediatR;

namespace Mediaspot.Application.Assets.Commands.UpdateMetadata;

public sealed class UpdateMetadataHandler(IAssetRepository repo, IUnitOfWork uow)
    : IRequestHandler<UpdateMetadataCommand>
{
    public async Task Handle(UpdateMetadataCommand request, CancellationToken ct)
    {
        var asset = await repo.GetAsync(request.AssetId, ct) ?? throw new KeyNotFoundException("Asset not found");

        asset.UpdateMetadata(new Metadata(request.Title, request.Description, request.Language));

        await uow.SaveChangesAsync(ct);
    }
}
