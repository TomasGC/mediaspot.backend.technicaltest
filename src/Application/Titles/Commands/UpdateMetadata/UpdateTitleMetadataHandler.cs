using Mediaspot.Application.Common;
using Mediaspot.Domain.Titles.Enums;
using MediatR;

namespace Mediaspot.Application.Titles.Commands.UpdateMetadata;

public sealed class UpdateTitleMetadataHandler(ITitleRepository repo, IUnitOfWork uow)
    : IRequestHandler<UpdateTitleMetadataCommand>
{
    public async Task Handle(UpdateTitleMetadataCommand request, CancellationToken ct)
    {
        var title = await repo.GetAsync(request.TitleId, ct);

        if (title == null)
        {
            throw new KeyNotFoundException("Title not found");
        }

        if (title.Type == TitleType.TvShow && (!request.SeasonNumber.HasValue || request.SeasonNumber.Value == 0))
        {
            throw new InvalidOperationException($"Title of type '{title.Type}' must have a season number value.");
        }
        else if (title.Type != TitleType.TvShow && request.SeasonNumber.HasValue)
        {
            throw new InvalidOperationException($"Title of type '{title.Type}' can't have a season number value.");
        }

        title.UpdateMetadata(new(
                request.Name,
                new(request.OriginCountry, request.OriginalLanguage),
                request.Description,
                request.SeasonNumber));

        await uow.SaveChangesAsync(ct);
    }
}
