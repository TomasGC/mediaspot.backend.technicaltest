using Mediaspot.Api.Responses.Titles.Models;
using Mediaspot.Domain.Titles.Enums;

namespace Mediaspot.Api.Responses.Titles;

public sealed record GetTitleResponse(Guid Id, string ExternalId, TitleType Type, Metadata Metadata, DateTime ReleaseDate) : 
    Title(Id, ExternalId, Type, Metadata, ReleaseDate)
{
}
