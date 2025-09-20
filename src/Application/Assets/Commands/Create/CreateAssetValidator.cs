using FluentValidation;

namespace Mediaspot.Application.Assets.Commands.Create;

public sealed class CreateAssetValidator : AbstractValidator<CreateAssetCommand>
{
    public CreateAssetValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
    }
}
