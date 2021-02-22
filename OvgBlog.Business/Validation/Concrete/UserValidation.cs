using FluentValidation;
using OvgBlog.Business.Validation.Abstract;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OvgBlog.Business.Validation.Concrete
{
    public class UserValidation : AbstractValidator<User>, IValidation
    {
        public UserValidation()
        {
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Name).NotNull();
            RuleFor(u => u.Name).NotEmpty();
            RuleFor(u => u.Name).MinimumLength(3);
            RuleFor(u => u.Password).NotNull();
            RuleFor(u => u.Password).NotEmpty();
            
        }
    }
}
