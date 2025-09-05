using FluentValidation;

namespace OvgBlog.Business.Dto.Validations;

public class ValidatorCreateCategoryDto : AbstractValidator<CreateCategoryDto>
{
    public ValidatorCreateCategoryDto()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.SeoUrl).NotNull().NotEmpty();
        RuleFor(x => x.ImageUrl).NotNull().NotEmpty();
    }
}