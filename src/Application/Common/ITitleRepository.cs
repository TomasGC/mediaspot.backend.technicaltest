using Mediaspot.Domain.Titles;

namespace Mediaspot.Application.Common;

public interface ITitleRepository
{
    Task<Title?> GetAsync(Guid id, CancellationToken ct);
    Task<Title?> GetByExternalIdAsync(string externalId, CancellationToken ct);
    Task AddAsync(Title title, CancellationToken ct);
}
