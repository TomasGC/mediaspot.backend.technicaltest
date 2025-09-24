using Mediaspot.Api.Responses.Models;

namespace Mediaspot.Api.Responses.Assets;

public sealed record CreateAssetResponse(Guid Id) : CreateResponse(Id) { }
