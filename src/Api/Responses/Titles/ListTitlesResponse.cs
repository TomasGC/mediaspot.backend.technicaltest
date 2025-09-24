using Mediaspot.Api.Responses.Titles.Models;

namespace Mediaspot.Api.Responses.Titles;

public sealed record ListTitlesResponse(IEnumerable<Title> Items) { }
