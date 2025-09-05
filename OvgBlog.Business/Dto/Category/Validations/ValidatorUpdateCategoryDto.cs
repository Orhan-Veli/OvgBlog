using System;
using FluentValidation;

namespace OvgBlog.Business.Dto.Validations;

public class ValidatorUpdateCategoryDto: AbstractValidator<UpdateCategoryDto>
{
    public ValidatorUpdateCategoryDto()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.SeoUrl).NotNull().NotEmpty();
        RuleFor(x => x.ImageUrl).NotNull().NotEmpty();
    }
}
    