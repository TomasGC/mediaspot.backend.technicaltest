using Mediaspot.Domain.Common;
using Mediaspot.Domain.Titles;
using Mediaspot.Domain.Titles.Filters;

namespace Mediaspot.Application.Common;

public interface ITitleRepository
{
    Task<List<Title>> ListAsync(ListTitleQueryFilters filters, ushort page, ushort pageSize, CancellationToken ct);
    Task<Title?> GetAsync(Guid id, CancellationToken ct);
    Task<Title?> GetByExternalIdAsync(string externalId, CancellationToken ct);
    Task AddAsync(Title title, CancellationToken ct);
}
