using FluentValidation;

namespace AuthAPI.Dto;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username can not be empty!");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty!");
        RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password should be minimum 8 character!");
    }





}
