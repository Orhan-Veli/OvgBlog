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
        Task<IResult<Article>> CreateAsync(CreateArticleDto dto, CancellationToken cancellationToken);

        Task<IResult<object>> DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<IResult<ArticleDto>> UpdateAsync(UpdateArticleDto dto, CancellationToken cancellationToken);

        Task<IResult<List<Article>>> GetAllAsync(CancellationToken cancellationToken);

        Task<IResult<Article>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<Article>> GetBySeoUrlAsync(string seoUrl, CancellationToken cancellationToken);

        Task<IResult<List<Article>>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);
    }
}
