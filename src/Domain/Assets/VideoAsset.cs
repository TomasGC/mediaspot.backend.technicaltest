using Mediaspot.Domain.Assets.Enums;
using Mediaspot.Domain.Assets.Events;
using Mediaspot.Domain.Assets.ValueObjects;

namespace Mediaspot.Domain.Assets;

public sealed class VideoAsset : BaseAsset
{
    public ushort Duration { get; set; }

    /// <summary>
    /// SD, HD, FHD, QHD, 2k, 4k, 8k.
    /// </summary>
    public string Resolution { get; set; }

    /// <summary>
    /// 23.98, 24, 25, 29.97 FPS.
    /// </summary>
    public float FrameRate { get; set; }

    /// <summary>
    /// MPEG-2, H.263, etc.
    /// </summary>
    public string Codec { get; set; }

    public VideoAsset() : base()
    {
        Codec = string.Empty;
    }

    public VideoAsset(
        string externalId,
        Metadata metadata,
        ushort duration,
        string resolution,
        float frameRate,
        string codec) : base(externalId, metadata, AssetType.Video)
    {
        Duration = duration;
        Resolution = resolution;
        FrameRate = frameRate;
        Codec = codec;

        Raise(new AssetCreated(Id, Type));
    }
}
