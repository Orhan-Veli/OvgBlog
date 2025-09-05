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
    public interface ITagService
    {
        Task<IResult<TagDto>> CreateAsync(CreateTagDto dto, CancellationToken cancellationToken);
        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<TagDto>> UpdateAsync(UpdateTagDto dto, CancellationToken cancellationToken);
        Task<IResult<IEnumerable<TagDto>>> GetAllAsync(TagFilterDto filterDto, CancellationToken cancellationToken);
        Task<IResult<TagDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<TagDto>> FindIdByNameAsync(string name, CancellationToken cancellationToken);

        Task<IResult<IEnumerable<TagDto>>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);

    }
}
