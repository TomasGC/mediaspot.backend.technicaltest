namespace Mediaspot.Api.DTOs.Assets;

public sealed record CreateAssetDto(string ExternalId, string Title, string? Description, string? Language);
