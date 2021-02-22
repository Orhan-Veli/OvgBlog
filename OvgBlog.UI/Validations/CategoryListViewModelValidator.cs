using FluentValidation;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Validations
{
    public class CategoryListViewModelValidator: AbstractValidator<CategoryListViewModel>
    {
        public CategoryListViewModelValidator()
        {
            RuleFor(c=> c.Id).NotNull();
            RuleFor(c=> c.Id).NotEqual(Guid.Empty);
            RuleFor(c=> c.Name).NotNull();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MaximumLength(20);
            RuleFor(c => c.SeoUrl).NotNull();
            RuleFor(c => c.SeoUrl).NotEmpty();
            RuleFor(c => c.SeoUrl).MaximumLength(20);
 
        }
    }
}
