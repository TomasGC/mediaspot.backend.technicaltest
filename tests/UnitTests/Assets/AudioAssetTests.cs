using Mediaspot.Domain.Assets;
using Mediaspot.Domain.Assets.ValueObjects;

namespace Mediaspot.UnitTests.Assets;

public class AudioAssetTests : BaseAssetTests<AudioAsset>
{
    protected override AudioAsset GetAssetObject(string externalId, Metadata metadata)
    {
        return new AudioAsset(externalId, metadata, 1000, 320, 48000, "7.1");
    }
}
