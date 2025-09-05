using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.Business.Dto;

namespace OvgBlog.Business.Abstract
{
    public interface IArticleService
    {
        Task<IResult<ArticleDto>> CreateAsync(CreateArticleDto dto, CancellationToken cancellationToken);

        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<IResult<ArticleDto>> UpdateAsync(UpdateArticleDto dto, CancellationToken cancellationToken);

        Task<IResult<List<ArticleDto>>> GetAllAsync(ArticleFilterDto filterDto, CancellationToken cancellationToken);

        Task<IResult<ArticleDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<ArticleDto>> GetBySeoUrlAsync(string seoUrl, CancellationToken cancellationToken);

        Task<IResult<List<ArticleDto>>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);
    }
}
