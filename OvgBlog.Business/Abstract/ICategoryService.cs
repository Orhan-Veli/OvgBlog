using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IResult<Category>> Create(Category category);

        Task<IResult<object>> Delete(Guid id);

        Task<IResult<Category>> Update(Category category);

        Task<IResult<IEnumerable<Category>>> GetAll();

        Task<IResult<Category>> GetById(Guid id);

        Task<IResult<Category>> CategoryBySeoUrl(string seoUrl);

    }
}
