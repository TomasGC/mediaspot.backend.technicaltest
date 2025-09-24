using Mediaspot.Domain.Titles.Enums;

namespace Mediaspot.Api.Responses.Titles.Models;

public record Title(Guid Id, string ExternalId, TitleType Type, Metadata Metadata, DateTime ReleaseDate) { }
