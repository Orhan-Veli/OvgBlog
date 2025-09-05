using System;
using FluentValidation;

namespace OvgBlog.Business.Dto.Validations;

public class ValidatorCreateArticleCategoryRelationDto : AbstractValidator<CreateArticleCategoryRelationDto>
{
    public ValidatorCreateArticleCategoryRelationDto()
    {
        RuleFor(x => x.ArticleId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.CategoryId).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}