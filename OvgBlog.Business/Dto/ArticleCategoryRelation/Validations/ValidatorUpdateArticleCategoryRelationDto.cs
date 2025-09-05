using System;
using FluentValidation;

namespace OvgBlog.Business.Dto.Validations;

public class ValidatorUpdateArticleCategoryRelationDto : AbstractValidator<UpdateArticleCategoryRelationDto>
{
    public ValidatorUpdateArticleCategoryRelationDto()
    {
        RuleFor(x => x.ArticleId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.CategoryId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}