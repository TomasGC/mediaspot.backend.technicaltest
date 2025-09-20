using Mediaspot.Application.Common;
using Mediaspot.Domain.Assets.ValueObjects;
using MediatR;

namespace Mediaspot.Application.Assets.Commands.RegisterMediaFile;

public sealed class RegisterMediaFileHandler(IAssetRepository repo, IUnitOfWork uow)
    : IRequestHandler<RegisterMediaFileCommand, Guid>
{
    public async Task<Guid> Handle(RegisterMediaFileCommand request, CancellationToken ct)
    {
        var asset = await repo.GetAsync(request.AssetId, ct) ?? throw new KeyNotFoundException("Asset not found");

        var mf = asset.RegisterMediaFile(new FilePath(request.Path), Duration.FromSeconds(request.DurationSeconds));
        await uow.SaveChangesAsync(ct);
        return mf.Id.Value;
    }
}
