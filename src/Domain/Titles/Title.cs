using Mediaspot.Domain.Common;
using Mediaspot.Domain.Titles.Enums;
using Mediaspot.Domain.Titles.ValueObjects;

namespace Mediaspot.Domain.Titles;

public sealed class Title : AggregateRoot
{
    public string ExternalId { get; private set; }

    public TitleType Type { get; private set; }

    public Metadata Metadata { get; private set; }

    public DateTime ReleaseDate { get; private set; } = DateTime.UtcNow;

    private Title()
    {
        ExternalId = string.Empty;
        Type = TitleType.Movie;
        Metadata = new(string.Empty, new(string.Empty, string.Empty), null, null);
    }

    public Title(
        string externalId,
        TitleType type,
        Metadata metadata)
    {
        ExternalId = externalId;
        Type = type;
        Metadata = metadata;
    }
}
