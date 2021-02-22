using FluentValidation;
using OvgBlog.Business.Validation.Abstract;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OvgBlog.Business.Validation.Concrete
{
    public class TagValidation : AbstractValidator<Tag>, IValidation
    {
        public TagValidation()
        {
            RuleFor(t => t.Name).NotNull();
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Name).MinimumLength(3);
            RuleFor(t=>t.SeoUrl).NotNull();
            RuleFor(t=>t.SeoUrl).NotEmpty();
            RuleFor(t => t.SeoUrl).MinimumLength(3);
        }
    }
}
