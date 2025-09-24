using Mediaspot.Domain.Assets.Enums;

namespace Mediaspot.Api.Responses.Assets;

public record GetVideoAssetResponse : BaseGetAssetResponse
{
    public ushort Duration { get; set; }
    public string Resolution { get; set; }
    public float FrameRate { get; set; }
    public string Codec { get; set; }

    public GetVideoAssetResponse() : base()
    {
        Resolution = string.Empty;
        Codec = string.Empty;
    }

    public GetVideoAssetResponse(
        Guid id,
        string externalId,
        AssetType type,
        Domain.Assets.ValueObjects.Metadata metadata,
        bool archived,
        ushort duration,
        string resolution,
        float frameRate,
        string codec) : base(id, externalId, type, metadata, archived)
    {
        Duration = duration;
        Resolution = resolution;
        FrameRate = frameRate;
        Codec = codec;
    }
}