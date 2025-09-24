namespace Mediaspot.Api.Responses.Assets;

public static class DomainBaseAssetExtensions
{
    public static BaseGetAssetResponse ToResponse(this Domain.Assets.AudioAsset asset)
    {
        return new GetAudioAssetResponse(
            asset.Id,
            asset.ExternalId,
            asset.Type,
            asset.Metadata,
            asset.Archived,
            asset.Duration,
            asset.Bitrate,
            asset.SampleRate,
            asset.Channels);
    }

    public static BaseGetAssetResponse ToResponse(this Domain.Assets.VideoAsset asset)
    {
        return new GetVideoAssetResponse(
            asset.Id,
            asset.ExternalId,
            asset.Type,
            asset.Metadata,
            asset.Archived,
            asset.Duration,
            asset.Resolution,
            asset.FrameRate,
            asset.Codec);
    }
}
