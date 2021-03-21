using FluentValidation;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Validations
{
    public class ContactListViewModelValidator:AbstractValidator<ContactListViewModel>
    {
        public ContactListViewModelValidator()
        {        
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Body).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        }
    }
}
