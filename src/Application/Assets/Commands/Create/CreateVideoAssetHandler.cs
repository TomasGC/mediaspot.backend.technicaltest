using Mediaspot.Application.Common;
using Mediaspot.Domain.Assets;
using Mediaspot.Domain.Assets.ValueObjects;
using MediatR;

namespace Mediaspot.Application.Assets.Commands.Create;

public sealed class CreateVideoAssetHandler(IAssetRepository repo, IUnitOfWork uow)
    : IRequestHandler<CreateVideoAssetCommand, Guid>
{
    public async Task<Guid> Handle(CreateVideoAssetCommand request, CancellationToken ct)
    {
        // Enforce uniqueness of ExternalId
        var existing = await repo.GetByExternalIdAsync(request.ExternalId, ct);
        if (existing is not null)
            throw new InvalidOperationException($"Asset with ExternalId '{request.ExternalId}' already exists.");

        var asset = new VideoAsset(
            request.ExternalId,
            new Metadata(request.Title, request.Description, request.Language),
            request.Duration,
            request.Resolution,
            request.FrameRate,
            request.Codec);

        await repo.AddAsync(asset, ct);
        await uow.SaveChangesAsync(ct);
        return asset.Id;
    }
}
