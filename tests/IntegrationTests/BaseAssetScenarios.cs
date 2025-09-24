using Mediaspot.Api.DTOs.Assets;
using Mediaspot.Api.Responses.Assets;
using Mediaspot.Domain.Assets.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net.Http.Json;
using System.Text.Json;

namespace Mediaspot.IntegrationTests;

public abstract class BaseAssetScenarios<TCreateDto>(WebApplicationFactory<Program> factory) : 
    BaseScenarios(factory), IClassFixture<WebApplicationFactory<Program>>
    where TCreateDto : BaseCreateAssetDto
{
    protected const string BASE_URL = "/assets";
    protected abstract AssetType _type { get; }

    [Fact]
    public async Task Post_Should_Create_Asset()
    {
        // Arrange
        var dto = GetCreateDtoObject($"Create - {_type}", "title", "description", "en");

        // Act
        var response = await _client.PostAsync(BASE_URL, JsonContent.Create<BaseCreateAssetDto>(dto));

        // Assert
        response.EnsureSuccessStatusCode();

        var result = JsonSerializer.Deserialize<CreateAssetResponse>(await response.Content.ReadAsStringAsync(), _options);
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task Get_Should_Provide_Asset()
    {
        // Arrange
        var dto = GetCreateDtoObject($"Get - {_type}", "title", "description", "en");
        var createResponse = await _client.PostAsync(BASE_URL, JsonContent.Create<BaseCreateAssetDto>(dto));
        var createResult = JsonSerializer.Deserialize<CreateAssetResponse>(await createResponse.Content.ReadAsStringAsync(), _options);

        // Act
        var response = await _client.GetAsync($"{BASE_URL}/{createResult!.Id}");

        // Assert
        response.EnsureSuccessStatusCode();

        await ValidateDtoAsync(dto, createResult.Id, response);
    }

    [Fact]
    public async Task UpdateMetadata_Should_Update_Metadata_Of_Asset()
    {
        // Arrange
        var createDto = GetCreateDtoObject($"Update - {_type}", "Old Title", "Old Description", "en");
        var createResponse = await _client.PostAsync(BASE_URL, JsonContent.Create<BaseCreateAssetDto>(createDto));
        var createResult = JsonSerializer.Deserialize<CreateAssetResponse>(await createResponse.Content.ReadAsStringAsync(), _options);

        var request = new UpdateAssetMetadataDto("New Name", "New Description", "it");
        var updatedDto = GetCreateDtoObject(createDto.ExternalId, "New Name", "New Description", "it");

        // Act
        var response = await _client.PutAsync($"{BASE_URL}/{createResult!.Id}/metadata", JsonContent.Create(request));

        // Assert
        response.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync($"{BASE_URL}/{createResult!.Id}");
        getResponse.EnsureSuccessStatusCode();

        await ValidateDtoAsync(updatedDto, createResult.Id, getResponse);
    }

    [Fact]
    public async Task RegisterMediaFile_Should_Provide_MediaFileId()
    {
        // Arrange
        var dto = GetCreateDtoObject($"RegisterMediaFile - {_type}", "title", "description", "en");
        var createResponse = await _client.PostAsync(BASE_URL, JsonContent.Create<BaseCreateAssetDto>(dto));
        var createResult = JsonSerializer.Deserialize<CreateAssetResponse>(await createResponse.Content.ReadAsStringAsync(), _options);

        var query = new RegisterMediaFileDto("media/file/path.mp4", 120.5);

        // Act
        var response = await _client.PostAsync($"{BASE_URL}/{createResult!.Id}/files?{DtoToQueryParams(query)}", null);

        // Assert
        response.EnsureSuccessStatusCode();

        var result = JsonSerializer.Deserialize<RegisterMediaFileResponse>(await response.Content.ReadAsStringAsync(), _options);
        result.ShouldNotBeNull();
        result.MediaFileId.ShouldNotBe(Guid.Empty);
    }

    protected void ValidateBaseAssetData(BaseCreateAssetDto dto, Guid id, BaseGetAssetResponse? response)
    {
        response.ShouldNotBeNull();
        response.Id.ShouldBe(id);
        response.ExternalId.ShouldBe(dto.ExternalId);
        response.Type.ShouldBe(_type);

        response.Metadata.ShouldNotBeNull();
        response.Metadata.Title.ShouldBe(dto.Title);
        response.Metadata.Description.ShouldBe(dto.Description);
        response.Metadata.Language.ShouldBe(dto.Language);
    }

    protected abstract TCreateDto GetCreateDtoObject(string externalId, string title, string description, string language);
    protected abstract Task ValidateDtoAsync(TCreateDto dto, Guid id, HttpResponseMessage response);
}
