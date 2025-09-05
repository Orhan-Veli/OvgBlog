using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.Business.Dto;

namespace OvgBlog.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IResult<CategoryDto>> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken);

        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<IResult<CategoryDto>> UpdateAsync(UpdateCategoryDto category, CancellationToken cancellationToken);

        Task<IResult<IEnumerable<CategoryDto>>> GetAllAsync(CategoryFilterDto filterDto, CancellationToken cancellationToken);

        Task<IResult<CategoryDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<CategoryDto>> GetCategoryIdByName(string name, CancellationToken cancellationToken);
    }
}
