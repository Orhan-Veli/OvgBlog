using System;
using FluentValidation;

namespace OvgBlog.Business.Dto.Contact.Validations;

public class ValidatorUpdateContactDto: AbstractValidator<UpdateContactDto>
{
    public ValidatorUpdateContactDto()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.Body).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.SendDate).NotNull().NotEmpty().NotEqual(DateTime.MinValue);
    }
}