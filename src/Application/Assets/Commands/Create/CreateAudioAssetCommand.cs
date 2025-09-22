namespace Mediaspot.Application.Assets.Commands.Create;

public sealed record CreateAudioAssetCommand(
    string ExternalId,
    string Title,
    string? Description,
    string? Language,
    ushort Duration,
    ushort Bitrate,
    ushort SampleRate,
    string Channels) : BaseCreateAssetCommand(ExternalId, Title, Description, Language);
