using FluentValidation;
using OvgBlog.UI.Models;
using System.Linq;

namespace OvgBlog.UI.Validations
{
    public class ArticleAddViewModelValidator:AbstractValidator<ArticleAddViewModel>
    {
        public ArticleAddViewModelValidator()
        {
            RuleFor(x => x.Body).NotNull().NotEmpty().MinimumLength(10);
            RuleFor(x => x.ImageUrl).NotNull().NotEmpty();
            RuleFor(x => x.SeoUrl).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(20);


            RuleFor(x => x.CategoryId).NotNull().NotEmpty();
            RuleFor(x => x.TagName).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(x => x.TagName).NotNull().NotEmpty().MaximumLength(30);


        }
    }
}
