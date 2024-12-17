using FluentValidation;

namespace ProfileManagement.Application.Profiles.Commands.CreateProfile;

public class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
{
    public CreateProfileCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(1).MaximumLength(4);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(6);
    }
}