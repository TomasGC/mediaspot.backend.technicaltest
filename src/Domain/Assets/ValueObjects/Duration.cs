namespace Mediaspot.Domain.Assets.ValueObjects;

public sealed record Duration(TimeSpan Value)
{
    public static Duration FromSeconds(double seconds)
        => new(TimeSpan.FromSeconds(seconds));
}
