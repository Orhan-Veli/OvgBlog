using FluentValidation;
using OvgBlog.Business.Validation.Abstract;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OvgBlog.Business.Validation.Concrete
{
    public class CategoryValidation : AbstractValidator<Category>, IValidation
    {
        public CategoryValidation()
        {
            RuleFor(c => c.Name).NotNull();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MaximumLength(20);
            RuleFor(c => c.SeoUrl).NotNull();
            RuleFor(c => c.SeoUrl).NotEmpty();
            RuleFor(c => c.SeoUrl).MaximumLength(20);
            RuleFor(c => c.ImageUrl).Empty().Equal("uploads/DefaultCategory.png");
        }
    }
}
