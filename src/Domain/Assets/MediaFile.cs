using Mediaspot.Domain.Assets.ValueObjects;
using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Assets;

public sealed class MediaFile : Entity
{
    public new MediaFileId Id { get; private set; }
    public FilePath Path { get; private set; }
    public Duration Duration { get; private set; }

    private MediaFile() { Id = MediaFileId.New(); Path = new(""); Duration = new(TimeSpan.Zero); }

    internal MediaFile(MediaFileId id, FilePath path, Duration duration)
    {
        Id = id;
        Path = path;
        Duration = duration;
    }
}
