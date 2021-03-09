using FluentValidation;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Validations
{
    public class CommentAddViewModelValidator:AbstractValidator<CommentAddViewModel>
    {
        public CommentAddViewModelValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(x => x.Body).NotNull().NotEmpty().MaximumLength(250);
            RuleFor(x => x.ArticleId).NotEqual(Guid.Empty);
        }
    }
}
