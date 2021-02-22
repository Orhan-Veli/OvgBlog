using FluentValidation;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Validations
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(l => l.UserName).NotNull();
            RuleFor(l => l.UserName).NotEmpty();
            RuleFor(l => l.UserName).MaximumLength(30);
            RuleFor(l => l.Password).NotNull();
            RuleFor(l => l.Password).NotEmpty();
            RuleFor(l => l.Password).MinimumLength(3);
        }
    }
}
