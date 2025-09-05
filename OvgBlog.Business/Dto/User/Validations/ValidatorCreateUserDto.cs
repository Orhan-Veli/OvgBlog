using FluentValidation;

namespace OvgBlog.Business.Dto.User.Validations;

public class ValidatorCreateUserDto : AbstractValidator<CreateUserDto>
{
    public ValidatorCreateUserDto()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
    
}