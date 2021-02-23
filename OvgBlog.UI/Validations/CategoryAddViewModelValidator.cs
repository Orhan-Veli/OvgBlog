using FluentValidation;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Validations
{
    public class CategoryAddViewModelValidator : AbstractValidator<CategoryAddViewModel>
    {
        public CategoryAddViewModelValidator()
        {
            RuleFor(ca => ca.Name).NotEmpty();
            RuleFor(ca => ca.Name).NotNull();
            RuleFor(ca => ca.Name).MinimumLength(2);
            RuleFor(ca => ca.SeoUrl).NotEmpty();
            RuleFor(ca => ca.SeoUrl).NotNull();
            RuleFor(ca => ca.SeoUrl).MinimumLength(2);
        }
    }
}
