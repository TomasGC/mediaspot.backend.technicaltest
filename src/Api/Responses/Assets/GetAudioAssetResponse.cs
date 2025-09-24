using Mediaspot.Domain.Assets.Enums;

namespace Mediaspot.Api.Responses.Assets;

public record GetAudioAssetResponse : BaseGetAssetResponse
{
    public ushort Duration { get; set; }
    public ushort Bitrate { get; set; }
    public ushort SampleRate { get; set; }
    public string Channels { get; set; }

    public GetAudioAssetResponse() : base()
    {
        Channels = string.Empty;
    }

    public GetAudioAssetResponse(
        Guid id,
        string externalId,
        AssetType type,
        Domain.Assets.ValueObjects.Metadata metadata,
        bool archived,
        ushort duration,
        ushort bitrate,
        ushort sampleRate,
        string channels) : base(id, externalId, type, metadata, archived)
    {
        Duration = duration;
        Bitrate = bitrate;
        SampleRate = sampleRate;
        Channels = channels;
    }
}
