using System;
using System.Data;
using FluentValidation;

namespace OvgBlog.Business.Dto.Contact.Validations;

public class ValidatorCreateContactDto : AbstractValidator<CreateContactDto>
{
    public ValidatorCreateContactDto()
    {
        RuleFor(x => x.Body).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}