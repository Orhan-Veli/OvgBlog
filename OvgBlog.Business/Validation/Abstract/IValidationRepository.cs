using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OvgBlog.Business.Validation.Abstract
{
    public interface IValidationRepository<T> where T : class,IValidation,new()
    {
        void GetValidate();
    }
}
