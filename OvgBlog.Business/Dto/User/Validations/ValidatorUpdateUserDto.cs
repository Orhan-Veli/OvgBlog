using System;
using FluentValidation;

namespace OvgBlog.Business.Dto.User.Validations;

public class ValidatorUpdateUserDto : AbstractValidator<UpdateUserDto>
{
    public ValidatorUpdateUserDto()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.Email).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
    
}