using Mediaspot.Application.Common;
using Mediaspot.Domain.Titles;
using MediatR;

namespace Mediaspot.Application.Titles.Commands.Create;

public sealed class CreateTitleHandler(ITitleRepository repo, IUnitOfWork uow)
    : IRequestHandler<CreateTitleCommand, Guid>
{
    public async Task<Guid> Handle(CreateTitleCommand request, CancellationToken ct)
    {
        // Enforce uniqueness of ExternalId
        var existing = await repo.GetByExternalIdAsync(request.ExternalId, ct);
        if (existing is not null)
        {
            throw new InvalidOperationException($"Title with ExternalId '{request.ExternalId}' already exists.");
        }

        var title = new Title(
            request.ExternalId,
            request.Type,
            new(request.Name, new(request.OriginCountry, request.OriginalLanguage),
            request.Description,
            request.SeasonNumber));

        await repo.AddAsync(title, ct);
        await uow.SaveChangesAsync(ct);

        return title.Id;
    }
}
