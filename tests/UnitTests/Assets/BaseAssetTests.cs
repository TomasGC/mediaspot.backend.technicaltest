using Mediaspot.Domain.Assets;
using Mediaspot.Domain.Assets.Events;
using Mediaspot.Domain.Assets.ValueObjects;
using Shouldly;

namespace Mediaspot.UnitTests.Assets;

public abstract class BaseAssetTests<T> where T : BaseAsset, new()
{
    private static readonly Metadata METADATA = new Metadata("t", null, null);

    [Fact]
    public void Constructor_Should_Set_Properties_And_Raise_AssetCreated()
    {
        var externalId = "ext-1";
        var metadata = new Metadata("title", "description", "en");
        var asset = GetAssetObject(externalId, metadata);

        asset.ExternalId.ShouldBe(externalId);
        asset.Metadata.ShouldBe(metadata);
        asset.DomainEvents.OfType<AssetCreated>().Any(ac => ac.Id == asset.Id).ShouldBeTrue();
    }

    [Fact]
    public void RegisterMediaFile_Should_Add_File_And_Raise_Event()
    {
        var asset = GetAssetObject("ext-2", METADATA);
        var path = new FilePath("/file.mp4");
        var duration = Duration.FromSeconds(10);

        var mf = asset.RegisterMediaFile(path, duration);

        asset.MediaFiles.ShouldContain(mf);
        asset.DomainEvents.OfType<MediaFileRegistered>().Any(reg => reg.AssetId == asset.Id && reg.Id == mf.Id.Value).ShouldBeTrue();
    }

    [Fact]
    public void UpdateMetadata_Should_Set_Metadata_And_Raise_Event()
    {
        var asset = GetAssetObject("ext-3", METADATA);
        var newMeta = new Metadata("new", "d", "fr");

        asset.UpdateMetadata(newMeta);

        asset.Metadata.ShouldBe(newMeta);
        asset.DomainEvents.OfType<MetadataUpdated>().Any(mu => mu.Id == asset.Id).ShouldBeTrue();
    }

    [Fact]
    public void UpdateMetadata_Should_Throw_If_Title_Empty()
    {
        var asset = GetAssetObject("ext-4", METADATA);
        var invalid = new Metadata("", null, null);

        Should.Throw<ArgumentException>(() => asset.UpdateMetadata(invalid));
    }

    [Fact]
    public void Archive_Should_Set_Archived_And_Raise_Event()
    {
        var asset = GetAssetObject("ext-5", METADATA);
        asset.Archive(_ => false);

        asset.Archived.ShouldBeTrue();
        asset.DomainEvents.OfType<AssetArchived>().Any(aa => aa.Id == asset.Id).ShouldBeTrue();
    }

    [Fact]
    public void Archive_Should_Throw_If_ActiveJobs()
    {
        var asset = GetAssetObject("ext-6", METADATA);
        Should.Throw<InvalidOperationException>(() => asset.Archive(_ => true));
    }

    [Fact]
    public void Archive_Should_Be_Idempotent()
    {
        var asset = GetAssetObject("ext-7", METADATA);
        asset.Archive(_ => false);
        asset.Archive(_ => false);
        asset.Archived.ShouldBeTrue();
        asset.DomainEvents.OfType<AssetArchived>().Count().ShouldBe(1);
    }

    protected abstract T GetAssetObject(string externalId, Metadata metadata);
}
