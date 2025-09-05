using System;
using FluentValidation;

namespace OvgBlog.Business.Dto.Validations;

public class ValidatorUpdateArticleDto : AbstractValidator<UpdateArticleDto>
{
    public ValidatorUpdateArticleDto()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty();
        RuleFor(x => x.Body).NotNull().NotEmpty();
        RuleFor(x => x.SeoUrl).NotNull().NotEmpty();
        RuleFor(x => x.RootPath).NotNull().NotEmpty();
        RuleFor(x => x.TagName).NotNull().NotEmpty().Must(x => x.Contains(','));
        RuleFor(x => x.CategoryId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.UserId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}