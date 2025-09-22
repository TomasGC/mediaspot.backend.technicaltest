using Mediaspot.Domain.Assets.Enums;
using Mediaspot.Domain.Assets.Events;
using Mediaspot.Domain.Assets.ValueObjects;
using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Assets;

public abstract class BaseAsset : AggregateRoot
{
    private readonly List<MediaFile> _mediaFiles = [];

    public string ExternalId { get; private set; }
    public AssetType Type { get; private set; }
    public Metadata Metadata { get; private set; }
    public bool Archived { get; private set; }

    public IReadOnlyCollection<MediaFile> MediaFiles => _mediaFiles.AsReadOnly();

    protected BaseAsset()
    {
        ExternalId = string.Empty;
        Metadata = new("", null, null);
    }

    protected BaseAsset(string externalId, Metadata metadata, AssetType type)
    {
        ExternalId = externalId;
        Metadata = metadata;
        Type = type;
    }

    public MediaFile RegisterMediaFile(FilePath path, Duration duration)
    {
        var mf = new MediaFile(MediaFileId.New(), path, duration);
        _mediaFiles.Add(mf);
        Raise(new MediaFileRegistered(Id, mf.Id.Value));
        
        Raise(new TranscodeRequested(Id, mf.Id.Value, "4K"));
        return mf;
    }

    public void UpdateMetadata(Metadata metadata)
    {
        // invariant: cannot set empty title
        if (string.IsNullOrWhiteSpace(metadata.Title))
            throw new ArgumentException("Title is required", nameof(metadata));

        Metadata = metadata;
        Raise(new MetadataUpdated(Id));
    }

    public void Archive(Func<Guid, bool> hasActiveJobs)
    {
        if (Archived) return;
        if (hasActiveJobs(Id))
            throw new InvalidOperationException("Cannot archive while active transcode jobs exist.");

        Archived = true;
        Raise(new AssetArchived(Id));
    }
}
