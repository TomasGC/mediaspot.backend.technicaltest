using Mediaspot.Domain.Assets;
using MediatR;

namespace Mediaspot.Application.Assets.Queries.GetById;

public sealed record GetAssetByIdQuery(Guid AssetId) : IRequest<Asset>;
