using FluentValidation;
using Mediaspot.Domain.Titles.Enums;

namespace Mediaspot.Application.Titles.Commands.Create;

public sealed class CreateTitleValidator : AbstractValidator<CreateTitleCommand>
{
    public CreateTitleValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Origin).SetValidator(new OriginValidator());

        When(x => x.Type == TitleType.TvShow, () =>
        {
            RuleFor(x => x.SeasonNumber).Must(x => x.HasValue && x.Value > 0);
        });

        When(x => x.Type != TitleType.TvShow, () =>
        {
            RuleFor(x => x.SeasonNumber).Must(x => !x.HasValue);
        });
    }
}
