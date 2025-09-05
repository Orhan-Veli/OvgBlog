using System;
using FluentValidation;

namespace OvgBlog.Business.Dto.Validations;

public class ValidatorUpdateArticleTagRelationDto : AbstractValidator<UpdateArticleTagRelationDto>
{
    public ValidatorUpdateArticleTagRelationDto()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.ArticleId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.TagId).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}