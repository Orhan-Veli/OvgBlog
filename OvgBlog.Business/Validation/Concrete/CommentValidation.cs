using FluentValidation;
using OvgBlog.Business.Validation.Abstract;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OvgBlog.Business.Validation.Concrete
{
    public class CommentValidation : AbstractValidator<Comment>, IValidation
    {
        public CommentValidation()
        {
            RuleFor(r => r.Name).NotNull();
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Name).MinimumLength(2);
            RuleFor(r => r.Email).NotNull();
            RuleFor(r => r.Email).NotEmpty();
            RuleFor(r => r.Email).EmailAddress();
            RuleFor(r => r.Body).NotNull();
            RuleFor(r => r.Body).NotEmpty();
            RuleFor(r => r.Body).MinimumLength(10);
        }
    }
}
