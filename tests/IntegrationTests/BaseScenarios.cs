using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;

namespace Mediaspot.IntegrationTests;

public class BaseScenarios
{
    protected readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    protected readonly HttpClient _client;

    protected BaseScenarios(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    protected static string DtoToQueryParams<T>(T dto)
    {
        var properties = dto!.GetType().GetProperties().Where(x => x.GetValue(dto, null) != null);
        var firstProp = properties.FirstOrDefault();
        if (firstProp == null)
        {
            return string.Empty;
        }

        string queryParams = $"{firstProp.Name.ToLower()}={firstProp.GetValue(dto)}";

        foreach (var prop in properties.Skip(1))
        {
            queryParams = $"{queryParams}&{prop.Name.ToLower()}={prop.GetValue(dto)}";
        }

        return queryParams;
    }
}