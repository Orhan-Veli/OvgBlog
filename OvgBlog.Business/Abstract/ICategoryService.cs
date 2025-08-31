using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IResult<Category>> Create(Category category, CancellationToken cancellationToken);

        Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken);

        Task<IResult<Category>> Update(Category category, CancellationToken cancellationToken);

        Task<IResult<IEnumerable<Category>>> GetAll(CancellationToken cancellationToken);

        Task<IResult<Category>> GetById(Guid id, CancellationToken cancellationToken);

        Task<IResult<Category>> CategoryBySeoUrl(string seoUrl, CancellationToken cancellationToken);

        Task<IResult<Category>> FindCategoryIdByName(string name, CancellationToken cancellationToken);

    }
}
