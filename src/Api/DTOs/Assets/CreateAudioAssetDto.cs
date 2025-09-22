using Mediaspot.Application.Assets.Commands.Create;

namespace Mediaspot.Api.DTOs.Assets;

public sealed record CreateAudioAssetDto(
    string ExternalId,
    string Title,
    string? Description,
    string? Language,
    ushort Duration,
    ushort Bitrate,
    ushort SampleRate,
    string Channels) : BaseCreateAssetDto(ExternalId, Title, Description, Language)
{
    public override BaseCreateAssetCommand ToCommand()
    {
        return new CreateAudioAssetCommand(
            ExternalId,
            Title,
            Description,
            Language,
            Duration,
            Bitrate,
            SampleRate,
            Channels);
    }
}
