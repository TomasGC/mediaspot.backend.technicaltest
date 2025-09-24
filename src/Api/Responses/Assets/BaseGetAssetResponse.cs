using Mediaspot.Api.Responses.Assets.Models;
using Mediaspot.Domain.Assets.Enums;
using System.Text.Json.Serialization;

namespace Mediaspot.Api.Responses.Assets;

[JsonDerivedType(typeof(GetAudioAssetResponse), typeDiscriminator: nameof(AssetType.Audio))]
[JsonDerivedType(typeof(GetVideoAssetResponse), typeDiscriminator: nameof(AssetType.Video))]
public abstract record BaseGetAssetResponse
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public AssetType Type { get; set; }
    public Metadata Metadata { get; set; }
    public bool Archived { get; set; }

    protected BaseGetAssetResponse()
    {
        ExternalId = string.Empty;
        Metadata = new(string.Empty, null, null);
    }

    protected BaseGetAssetResponse(
        Guid id,
        string externalId,
        AssetType type,
        Domain.Assets.ValueObjects.Metadata metadata,
        bool archived)
    {
        Id = id;
        ExternalId = externalId;
        Type = type;
        Metadata = new(metadata.Title, metadata.Description, metadata.Language);
        Archived = archived;
    }
}