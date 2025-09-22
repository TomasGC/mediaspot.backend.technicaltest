using Mediaspot.Application.Assets.Commands.Create;
using Mediaspot.Domain.Assets.Enums;
using System.Text.Json.Serialization;

namespace Mediaspot.Api.DTOs.Assets;

[JsonDerivedType(typeof(CreateAudioAssetDto), typeDiscriminator: nameof(AssetType.Audio))]
[JsonDerivedType(typeof(CreateVideoAssetDto), typeDiscriminator: nameof(AssetType.Video))]
public abstract record BaseCreateAssetDto(string ExternalId, string Title, string? Description, string? Language)
{
    public abstract BaseCreateAssetCommand ToCommand();
}
