using Mediaspot.Domain.Assets.Enums;
using Mediaspot.Domain.Assets.Events;
using Mediaspot.Domain.Assets.ValueObjects;

namespace Mediaspot.Domain.Assets;

public sealed class AudioAsset : BaseAsset
{
    public ushort Duration { get; set; }

    /// <summary>
    /// From 64 kbps to 320 kbps.
    /// </summary>
    public ushort Bitrate { get; set; }

    /// <summary>
    /// From 8 kHz to 48 Khz.
    /// </summary>
    public ushort SampleRate { get; set; }
    public string Channels { get; set; }

    public AudioAsset() : base()
    {
        Channels = string.Empty;
    }

    public AudioAsset(
        string externalId,
        Metadata metadata,
        ushort duration,
        ushort bitrate,
        ushort sampleRate,
        string channels) : base(externalId, metadata, AssetType.Audio)
    {
        Duration = duration;
        Bitrate = bitrate;
        SampleRate = sampleRate;
        Channels = channels;

        Raise(new AssetCreated(Id, Type));
    }
}
