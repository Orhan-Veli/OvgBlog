using System;
using FluentValidation;

namespace OvgBlog.Business.Dto.Validator;

public class ValidatorUpdateTagDto : AbstractValidator<UpdateTagDto>
{
    public ValidatorUpdateTagDto()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.SeoUrl).NotNull().NotEmpty();
    }
}