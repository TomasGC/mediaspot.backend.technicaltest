namespace Mediaspot.Domain.Titles.ValueObjects;

public sealed record Metadata(string Name, Origin Origin, string? Description, ushort? SeasonNumber)
{
    public Metadata() : this(string.Empty, new Origin(string.Empty, string.Empty), null, null) { }
}