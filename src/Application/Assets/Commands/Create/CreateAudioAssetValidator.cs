using FluentValidation;

namespace Mediaspot.Application.Assets.Commands.Create;

public sealed class CreateAudioAssetValidator : BaseCreateAssetValidator<CreateAudioAssetCommand>
{
    public CreateAudioAssetValidator() : base()
    {
        RuleFor(x => x.Duration).GreaterThan((ushort)0);
        RuleFor(x => x.Bitrate).GreaterThan((ushort)0);
        RuleFor(x => x.SampleRate).GreaterThan((ushort)0);
        RuleFor(x => x.Channels).NotEmpty();
    }
}
