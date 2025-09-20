using FluentValidation;
using Mediaspot.Application.Titles.Commands.UpdateMetadata;

namespace Mediaspot.Application.Titles.Commands.Create;

public sealed class UpdateTitleMetadataValidator : AbstractValidator<UpdateTitleMetadataCommand>
{
    public UpdateTitleMetadataValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.OriginCountry).NotEmpty();
        RuleFor(x => x.OriginalLanguage).NotEmpty();
    }
}
