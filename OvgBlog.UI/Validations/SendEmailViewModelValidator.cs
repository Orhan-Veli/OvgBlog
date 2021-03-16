using FluentValidation;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Validations
{
    public class SendEmailViewModelValidator:AbstractValidator<SendEmailViewModel>
    {
        public SendEmailViewModelValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(30).EmailAddress();
            RuleFor(x=> x.Body).NotEmpty().NotNull().MaximumLength(250);
        }
    }
}
