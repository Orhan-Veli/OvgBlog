using FluentValidation;
using OvgBlog.Business.Validation.Abstract;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OvgBlog.Business.Validation.Concrete
{
    public class ArticleValidation : AbstractValidator<Article>,IValidation
    {
        public ArticleValidation()
        {
            RuleFor(a => a.Title).NotNull();
            RuleFor(a => a.Title).NotEmpty();
            RuleFor(a => a.Title).MinimumLength(10);
            RuleFor(a => a.Body).NotNull();
            RuleFor(a => a.Body).NotEmpty();
            RuleFor(a => a.Body).MinimumLength(10);
            RuleFor(a => a.SeoUrl).NotNull();
            RuleFor(a => a.SeoUrl).NotEmpty();
            RuleFor(a => a.SeoUrl).MinimumLength(2);
            RuleFor(a => a.ImageUrl).NotNull();
            RuleFor(a => a.ImageUrl).NotEmpty();
            RuleFor(a => a.ImageUrl).MinimumLength(5);
        }
    }
}
