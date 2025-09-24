using Mediaspot.Api.DTOs.Titles;
using Mediaspot.Api.Responses.Titles;
using Mediaspot.Domain.Titles.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net.Http.Json;
using System.Text.Json;

namespace Mediaspot.IntegrationTests;

public class TitleScenarios(WebApplicationFactory<Program> factory) : 
    BaseScenarios(factory), IClassFixture<WebApplicationFactory<Program>>
{
    private const string BASE_URL = "/titles";

    [Fact]
    public async Task Post_Should_Create_Title()
    {
        // Arrange
        var dto = new CreateTitleDto("Create", TitleType.Movie, "name", "France", "French", "description", null);

        // Act
        var response = await _client.PostAsync(BASE_URL, JsonContent.Create(dto));

        // Assert
        var result = JsonSerializer.Deserialize<CreateTitleResponse>(await response.Content.ReadAsStringAsync(), _options);
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task Get_Should_Provide_Title()
    {
        // Arrange
        var dto = new CreateTitleDto("Get", TitleType.Movie, "name", "France", "French", "description", null);
        var createResponse = await _client.PostAsync( BASE_URL, JsonContent.Create(dto));
        var createResult = JsonSerializer.Deserialize<CreateTitleResponse>(await createResponse.Content.ReadAsStringAsync(), _options);

        // Act
        var response = await _client.GetAsync($"{BASE_URL}/{createResult!.Id}");

        // Assert
        var result = JsonSerializer.Deserialize<GetTitleResponse>(await response.Content.ReadAsStringAsync(), _options);
        result.ShouldNotBeNull();
        result.Id.ShouldBe(createResult.Id);
        result.ExternalId.ShouldBe(dto.ExternalId);
        result.Type.ShouldBe(dto.Type);

        result.Metadata.ShouldNotBeNull();
        result.Metadata.Name.ShouldBe(dto.Name);
        result.Metadata.Description.ShouldBe(dto.Description);
        result.Metadata.Origin.ShouldNotBeNull();
        result.Metadata.Origin.Country.ShouldBe(dto.OriginCountry);
        result.Metadata.Origin.Language.ShouldBe(dto.OriginalLanguage);
        result.Metadata.SeasonNumber.ShouldBe(dto.SeasonNumber);
    }

    [Fact]
    public async Task GetByExternalId_Should_Provide_Title()
    {
        // Arrange
        var dto = new CreateTitleDto("GetByExternalId", TitleType.Movie, "name", "France", "French", "description", null);
        var createResponse = await _client.PostAsync(BASE_URL, JsonContent.Create(dto));
        var createResult = JsonSerializer.Deserialize<CreateTitleResponse>(await createResponse.Content.ReadAsStringAsync(), _options);

        // Act
        var response = await _client.GetAsync($"{BASE_URL}/{dto!.ExternalId}");

        // Assert
        var result = JsonSerializer.Deserialize<GetTitleByExternalIdResponse>(await response.Content.ReadAsStringAsync(), _options);
        result.ShouldNotBeNull();
        result.Id.ShouldBe(createResult!.Id);
        result.ExternalId.ShouldBe(dto.ExternalId);
        result.Type.ShouldBe(dto.Type);

        result.Metadata.ShouldNotBeNull();
        result.Metadata.Name.ShouldBe(dto.Name);
        result.Metadata.Description.ShouldBe(dto.Description);
        result.Metadata.Origin.ShouldNotBeNull();
        result.Metadata.Origin.Country.ShouldBe(dto.OriginCountry);
        result.Metadata.Origin.Language.ShouldBe(dto.OriginalLanguage);
        result.Metadata.SeasonNumber.ShouldBe(dto.SeasonNumber);
    }

    [Fact]
    public async Task List_Should_Provide_All_Titles_Without_Parameters()
    {
        // Arrange
        var dto = new CreateTitleDto("ListWithoutParameter", TitleType.Movie, "name", "France", "French", "description", null);
        var createResponse = await _client.PostAsync(BASE_URL, JsonContent.Create(dto));
        var createResult = JsonSerializer.Deserialize<CreateTitleResponse>(await createResponse.Content.ReadAsStringAsync(), _options);
        var query = new ListTitlesDto();

        // Act
        var response = await _client.GetAsync($"{BASE_URL}?{DtoToQueryParams(query)}");

        // Assert
        var result = JsonSerializer.Deserialize<ListTitlesResponse>(await response.Content.ReadAsStringAsync(), _options);
        result.ShouldNotBeNull();
        result.Items.ShouldNotBeEmpty();

        var item = result.Items.Last();
        item.Id.ShouldBe(createResult!.Id);
        item.ExternalId.ShouldBe(dto.ExternalId);
        item.Type.ShouldBe(dto.Type);

        item.Metadata.ShouldNotBeNull();
        item.Metadata.Name.ShouldBe(dto.Name);
        item.Metadata.Description.ShouldBe(dto.Description);
        item.Metadata.Origin.ShouldNotBeNull();
        item.Metadata.Origin.Country.ShouldBe(dto.OriginCountry);
        item.Metadata.Origin.Language.ShouldBe(dto.OriginalLanguage);
        item.Metadata.SeasonNumber.ShouldBe(dto.SeasonNumber);
    }

    [Fact]
    public async Task List_Should_Provide_All_Titles_For_Given_Type()
    {
        // Arrange
        var dto = new CreateTitleDto("ListWithTypeParameter", TitleType.Movie, "name", "France", "French", "description", null);
        var createResponse = await _client.PostAsync(BASE_URL, JsonContent.Create(dto));
        var createResult = JsonSerializer.Deserialize<CreateTitleResponse>(await createResponse.Content.ReadAsStringAsync(), _options);
        var query = new ListTitlesDto(TitleType.Movie);

        // Act
        var response = await _client.GetAsync($"{BASE_URL}?{DtoToQueryParams(query)}");

        // Assert
        var result = JsonSerializer.Deserialize<ListTitlesResponse>(await response.Content.ReadAsStringAsync(), _options);
        result.ShouldNotBeNull();
        result.Items.ShouldNotBeEmpty();

        var item = result.Items.Last();
        item.Id.ShouldBe(createResult!.Id);
        item.ExternalId.ShouldBe(dto.ExternalId);
        item.Type.ShouldBe(dto.Type);

        item.Metadata.ShouldNotBeNull();
        item.Metadata.Name.ShouldBe(dto.Name);
        item.Metadata.Description.ShouldBe(dto.Description);
        item.Metadata.Origin.ShouldNotBeNull();
        item.Metadata.Origin.Country.ShouldBe(dto.OriginCountry);
        item.Metadata.Origin.Language.ShouldBe(dto.OriginalLanguage);
        item.Metadata.SeasonNumber.ShouldBe(dto.SeasonNumber);
    }

    [Fact]
    public async Task List_Should_Provide_All_Titles_For_Given_NamePattern()
    {
        // Arrange
        var dto = new CreateTitleDto("ListWithNamePatternParameter", TitleType.Movie, "Paddington", "France", "French", "description", null);
        var createResponse = await _client.PostAsync(BASE_URL, JsonContent.Create(dto));
        var createResult = JsonSerializer.Deserialize<CreateTitleResponse>(await createResponse.Content.ReadAsStringAsync(), _options);
        var query = new ListTitlesDto(NamePattern: "addi");

        // Act
        var response = await _client.GetAsync($"{BASE_URL}?{DtoToQueryParams(query)}");

        // Assert
        var result = JsonSerializer.Deserialize<ListTitlesResponse>(await response.Content.ReadAsStringAsync(), _options);
        result.ShouldNotBeNull();
        result.Items.ShouldNotBeEmpty();

        var item = result.Items.Last();
        item.Id.ShouldBe(createResult!.Id);
        item.ExternalId.ShouldBe(dto.ExternalId);
        item.Type.ShouldBe(dto.Type);

        item.Metadata.ShouldNotBeNull();
        item.Metadata.Name.ShouldBe(dto.Name);
        item.Metadata.Description.ShouldBe(dto.Description);
        item.Metadata.Origin.ShouldNotBeNull();
        item.Metadata.Origin.Country.ShouldBe(dto.OriginCountry);
        item.Metadata.Origin.Language.ShouldBe(dto.OriginalLanguage);
        item.Metadata.SeasonNumber.ShouldBe(dto.SeasonNumber);
    }

    [Fact]
    public async Task List_Should_Provide_0_Title_With_Wrong_Parameter()
    {
        // Arrange
        var dto = new CreateTitleDto("ListWithWrongParameter", TitleType.Movie, "name", "France", "French", "description", null);
        var createResponse = await _client.PostAsync(BASE_URL, JsonContent.Create(dto));
        _ = JsonSerializer.Deserialize<CreateTitleResponse>(await createResponse.Content.ReadAsStringAsync(), _options);
        var query = new ListTitlesDto(TitleType.TvShow);

        // Act
        var response = await _client.GetAsync($"{BASE_URL}?{DtoToQueryParams(query)}");

        // Assert
        response.EnsureSuccessStatusCode();

        var result = JsonSerializer.Deserialize<ListTitlesResponse>(await response.Content.ReadAsStringAsync(), _options);
        result.ShouldNotBeNull();
        result.Items.ShouldBeEmpty();
    }

    [Fact]
    public async Task UpdateMetadata_Should_Update_Metadata_Of_Title()
    {
        // Arrange
        var dto = new CreateTitleDto("UpdateMetadata", TitleType.Movie, "Old Name", "France", "French", "Old Description", null);
        var createResponse = await _client.PostAsync(BASE_URL, JsonContent.Create(dto));
        var createResult = JsonSerializer.Deserialize<CreateTitleResponse>(await createResponse.Content.ReadAsStringAsync(), _options);

        var request = new UpdateTitleMetadataDto("New Name", dto.OriginCountry, dto.OriginalLanguage, "New Description", null);

        // Act
        var response = await _client.PutAsync($"{BASE_URL}/{createResult!.Id}/metadata", JsonContent.Create(request));

        // Assert
        response.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync($"{BASE_URL}/{createResult!.Id}");
        var result = JsonSerializer.Deserialize<GetTitleResponse>(await getResponse.Content.ReadAsStringAsync(), _options);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(createResult!.Id);
        result.ExternalId.ShouldBe(dto.ExternalId);
        result.Type.ShouldBe(dto.Type);

        result.Metadata.ShouldNotBeNull();
        result.Metadata.Name.ShouldBe(request.Name);
        result.Metadata.Description.ShouldBe(request.Description);
        result.Metadata.Origin.ShouldNotBeNull();
        result.Metadata.Origin.Country.ShouldBe(request.OriginCountry);
        result.Metadata.Origin.Language.ShouldBe(request.OriginalLanguage);
        result.Metadata.SeasonNumber.ShouldBe(request.SeasonNumber);
    }
}
