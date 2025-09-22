using Mediaspot.Domain.Assets;
using Mediaspot.Domain.Assets.ValueObjects;

namespace Mediaspot.UnitTests.Assets;

public class VideoAssetTests : BaseAssetTests<VideoAsset>
{
    protected override VideoAsset GetAssetObject(string externalId, Metadata metadata)
    {
        return new VideoAsset(externalId, metadata, 1000, "4k", 24, "MPEG-2");
    }
}
