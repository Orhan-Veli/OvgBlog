using FluentValidation;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Validations
{
    public class CommentViewModelValidator : AbstractValidator<CommentViewModel>
    {
        public CommentViewModelValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(c => c.Email).NotNull();
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.Name).NotNull();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MaximumLength(20);
            RuleFor(c => c.Body).NotNull();
            RuleFor(c => c.Body).NotEmpty();
            RuleFor(c => c.Body).MinimumLength(10);
        }

    }
}
