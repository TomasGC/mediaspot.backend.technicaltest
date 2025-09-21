using FluentValidation;
using Mediaspot.Domain.Titles.ValueObjects;

namespace Mediaspot.Application.Titles.Commands.Create;

public sealed class OriginValidator : AbstractValidator<Origin>
{
    public OriginValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.Language).NotEmpty();
    }
}
