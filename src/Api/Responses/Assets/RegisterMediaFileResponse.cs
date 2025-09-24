using Mediaspot.Api.Responses.Models;

namespace Mediaspot.Api.Responses.Assets;

public sealed record RegisterMediaFileResponse(Guid MediaFileId) : CreateResponse(MediaFileId) { }
