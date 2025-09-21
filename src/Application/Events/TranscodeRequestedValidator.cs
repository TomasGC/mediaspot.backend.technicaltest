using FluentValidation;
using Mediaspot.Domain.Assets.Events;

namespace Mediaspot.Application.Events;

public sealed class TranscodeRequestedValidator : AbstractValidator<TranscodeRequested>
{
    public TranscodeRequestedValidator()
    {
        RuleFor(x => x.TargetPreset).NotEmpty();
    }
}
