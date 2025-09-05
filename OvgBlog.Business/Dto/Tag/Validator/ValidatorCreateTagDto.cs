using FluentValidation;

namespace OvgBlog.Business.Dto.Validator;

public class ValidatorCreateTagDto : AbstractValidator<CreateTagDto>
{
    public ValidatorCreateTagDto()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.SeoUrl).NotNull().NotEmpty();
    }
}