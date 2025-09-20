using Mediaspot.Application.Common;
using MediatR;

namespace Mediaspot.Application.Assets.Commands.Archive;

public sealed class ArchiveAssetHandler(IAssetRepository repo, ITranscodeJobRepository jobs, IUnitOfWork uow)
    : IRequestHandler<ArchiveAssetCommand>
{
    public async Task Handle(ArchiveAssetCommand request, CancellationToken ct)
    {
        var asset = await repo.GetAsync(request.AssetId, ct) ?? throw new KeyNotFoundException("Asset not found");

        bool hasActive = await jobs.HasActiveJobsAsync(asset.Id, ct);
        asset.Archive(_ => hasActive);

        await uow.SaveChangesAsync(ct);
    }
}
