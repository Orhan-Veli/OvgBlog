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
        Task<IResult<Category>> CreateAsync(Category category, CancellationToken cancellationToken);

        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<IResult<Category>> UpdateAsync(Category category, CancellationToken cancellationToken);

        Task<IResult<IEnumerable<Category>>> GetAllAsync(CancellationToken cancellationToken);

        Task<IResult<Category>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IResult<Category>> CategoryBySeoUrlAsync(string seoUrl, CancellationToken cancellationToken);

        Task<IResult<Category>> FindCategoryIdByName(string name, CancellationToken cancellationToken);

    }
}
