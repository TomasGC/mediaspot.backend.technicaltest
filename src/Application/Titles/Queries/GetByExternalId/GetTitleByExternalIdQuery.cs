using Mediaspot.Domain.Titles;
using MediatR;

namespace Mediaspot.Application.Titles.Queries.GetByExternalId;

public sealed record GetTitleByExternalIdQuery(string TitleExternalId) : IRequest<Title>;
