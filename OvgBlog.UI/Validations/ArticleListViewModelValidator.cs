using FluentValidation;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Validations
{
    public class ArticleListViewModelValidator:AbstractValidator<ArticleListViewModel>
    {
        public ArticleListViewModelValidator()
        {
            RuleFor(a => a.Id).NotNull();
            RuleFor(a => a.Id).NotEqual(Guid.Empty);
            RuleFor(a => a.Title).NotNull();
            RuleFor(a => a.Title).NotEmpty();
            RuleFor(a => a.Title).MaximumLength(20);
            RuleFor(a => a.Body).NotEmpty();
            RuleFor(a => a.Body).NotNull();
            RuleFor(a => a.Body).MinimumLength(10);
            RuleFor(a => a.SeoUrl).NotNull();
            RuleFor(a => a.SeoUrl).NotEmpty();
            RuleFor(a => a.SeoUrl).MaximumLength(30);
        }

    }
}
