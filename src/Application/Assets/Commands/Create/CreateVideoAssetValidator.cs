using FluentValidation;

namespace Mediaspot.Application.Assets.Commands.Create;

public sealed class CreateVideoAssetValidator : BaseCreateAssetValidator<CreateVideoAssetCommand>
{
    public CreateVideoAssetValidator() : base()
    {
        RuleFor(x => x.Duration).GreaterThan((ushort)0);
        RuleFor(x => x.Resolution).NotEmpty();
        RuleFor(x => x.FrameRate).GreaterThan(0);
        RuleFor(x => x.Codec).NotEmpty();
    }
}
