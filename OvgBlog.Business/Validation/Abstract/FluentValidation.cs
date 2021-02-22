using FluentValidation;
using FluentValidation.Results;
using OvgBlog.Business.Validation.Concrete;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OvgBlog.Business.Validation.Abstract
{
    public class FluentValidation<TValidate>:IValidationRepository<TValidate> where TValidate: class,IValidation,new()
      
    {
        private readonly object baseEntity;
        public FluentValidation(object entity)
        {
            baseEntity = entity;
        }
        public void GetValidate()
        {
           var context = new ValidationContext<object>(baseEntity);
           TValidate validate = new TValidate();
           ValidationResult result = validate.Validate(context);
           if (!result.IsValid)
           {
             throw new ValidationException(result.Errors);
           }
        }
    }
   
}
