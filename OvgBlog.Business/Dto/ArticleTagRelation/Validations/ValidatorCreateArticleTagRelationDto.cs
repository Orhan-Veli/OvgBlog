using System;
using FluentValidation;

namespace OvgBlog.Business.Dto.Validations;

public class ValidatorCreateArticleTagRelationDto : AbstractValidator<CreateArticleTagRelationDto>
{
    public ValidatorCreateArticleTagRelationDto()
    {
        RuleFor(x => x.ArticleId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.TagId).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}