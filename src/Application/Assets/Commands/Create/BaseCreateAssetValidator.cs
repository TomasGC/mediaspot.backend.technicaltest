using FluentValidation;

namespace Mediaspot.Application.Assets.Commands.Create;

public abstract class BaseCreateAssetValidator<T> : AbstractValidator<T> where T : BaseCreateAssetCommand
{
    protected BaseCreateAssetValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
    }
}
