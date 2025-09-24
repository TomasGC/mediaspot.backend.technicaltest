using Mediaspot.Api.DTOs.Assets;
using Mediaspot.Api.Responses.Assets;
using Mediaspot.Domain.Assets.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Text.Json;

namespace Mediaspot.IntegrationTests;

public class VideoAssetScenarios(WebApplicationFactory<Program> factory) : BaseAssetScenarios<CreateVideoAssetDto>(factory)
{
    protected override AssetType _type { get; } = AssetType.Video;

    protected override CreateVideoAssetDto GetCreateDtoObject(string externalId, string title, string description, string language)
    {
        return new(externalId, title, description, language, 1000, "FHD", 24, "MPEG-2");
    }

    protected override async Task ValidateDtoAsync(CreateVideoAssetDto dto, Guid id, HttpResponseMessage response)
    {
        var result = JsonSerializer.Deserialize<GetVideoAssetResponse>(await response.Content.ReadAsStringAsync(), _options);
        ValidateBaseAssetData(dto, id, result);

        result!.Duration.ShouldBe(dto.Duration);
        result.Resolution.ShouldBe(dto.Resolution);
        result.FrameRate.ShouldBe(dto.FrameRate);
        result.Codec.ShouldBe(dto.Codec);
    }
}
