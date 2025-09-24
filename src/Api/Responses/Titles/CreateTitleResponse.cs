using Mediaspot.Api.Responses.Models;

namespace Mediaspot.Api.Responses.Titles;

public sealed record CreateTitleResponse(Guid Id) : CreateResponse(Id) { }
