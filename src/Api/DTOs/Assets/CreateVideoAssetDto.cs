using Mediaspot.Application.Assets.Commands.Create;

namespace Mediaspot.Api.DTOs.Assets;

public sealed record CreateVideoAssetDto(
    string ExternalId,
    string Title,
    string? Description,
    string? Language,
    ushort Duration,
    string Resolution,
    float FrameRate,
    string Codec) : BaseCreateAssetDto(ExternalId, Title, Description, Language)
{
    public override BaseCreateAssetCommand ToCommand()
    {
        return new CreateVideoAssetCommand(
            ExternalId,
            Title,
            Description,
            Language,
            Duration,
            Resolution,
            FrameRate,
            Codec);
    }
}
