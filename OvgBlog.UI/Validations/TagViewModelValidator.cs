using FluentValidation;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Validations
{
    public class TagViewModelValidator:AbstractValidator<TagViewModel>
    {
        public TagViewModelValidator()
        {
            RuleFor(x=>x.Name).NotNull().NotEmpty();
            RuleFor(x => x.SeoUrl).NotNull().NotEmpty();
        }
    }
}
