namespace Mediaspot.Domain.Assets.ValueObjects;

public sealed record FilePath(string Value)
{
    public override string ToString() => Value;
}
