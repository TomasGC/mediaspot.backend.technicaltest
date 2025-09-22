namespace Mediaspot.Application.Assets.Commands.Create;

public sealed record CreateVideoAssetCommand(
    string ExternalId,
    string Title,
    string? Description,
    string? Language,
    ushort Duration,
    string Resolution,
    float FrameRate,
    string Codec) : BaseCreateAssetCommand(ExternalId, Title, Description, Language);
