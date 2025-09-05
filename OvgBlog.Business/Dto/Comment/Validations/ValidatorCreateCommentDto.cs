using System;
using System.Data;
using FluentValidation;

namespace OvgBlog.Business.Dto.Comment.Validations;

public class ValidatorCreateCommentDto : AbstractValidator<CreateCommentDto>
{
    public ValidatorCreateCommentDto()
    {
        RuleFor(x => x.ArticleId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty();
        RuleFor(x => x.Body).NotNull().NotEmpty();
    }
}