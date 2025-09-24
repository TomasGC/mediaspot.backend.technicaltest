using Mediaspot.Api.DTOs.Assets;
using Mediaspot.Api.Responses.Assets;
using Mediaspot.Domain.Assets.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Text.Json;

namespace Mediaspot.IntegrationTests;

public class AudioAssetScenarios(WebApplicationFactory<Program> factory) : BaseAssetScenarios<CreateAudioAssetDto>(factory)
{
    protected override AssetType _type { get; } = AssetType.Audio;

    protected override CreateAudioAssetDto GetCreateDtoObject(string externalId, string title, string description, string language)
    {
        return new(externalId, title, description, language, 1000, 320, 48000, "7.1");
    }

    protected override async Task ValidateDtoAsync(CreateAudioAssetDto dto, Guid id, HttpResponseMessage response)
    {
        var result = JsonSerializer.Deserialize<GetAudioAssetResponse>(await response.Content.ReadAsStringAsync(), _options);
        ValidateBaseAssetData(dto, id, result);

        result!.Duration.ShouldBe(dto.Duration);
        result.Bitrate.ShouldBe(dto.Bitrate);
        result.SampleRate.ShouldBe(dto.SampleRate);
        result.Channels.ShouldBe(dto.Channels);
    }
}