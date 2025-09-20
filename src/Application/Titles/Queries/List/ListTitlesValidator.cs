using FluentValidation;

namespace Mediaspot.Application.Titles.Queries.List;

public sealed class ListTitlesValidator : AbstractValidator<ListTitlesQuery>
{
    public ListTitlesValidator()
    {
        When(x => x.FromReleaseDate.HasValue && x.ToReleaseDate.HasValue, () =>
        {
            RuleFor(x => x.ToReleaseDate).GreaterThan(x => x.FromReleaseDate);
        });
    }
}
