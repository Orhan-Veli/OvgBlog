using FluentValidation;
using FluentValidation.Results;
using OvgBlog.Business.Validation.Concrete;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OvgBlog.Business.Validation.Abstract
{
    public class FluentValidation<TValidation,TEntity>
        where TValidation : class, new()
        where TEntity : class,new()
    {
        public FluentValidation(IValidator validator,TEntity entity)
        {
            //To do Fix Tomorrow
            //var context = new ValidationContext<object>(entity);
            //TEntity entity1 = new TEntity();
            //IValidation validation = new ArticleValidation();
            //IValidation validation = new TValidation();
            //ValidationResult result = validation.Validate(entity1);

            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}
            
        }

    }
   
}
