using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface IArticleService
    {
        Task<IResult<Article>> Create(Article article, CancellationToken cancellationToken);

        Task<IResult<object>> Delete(Guid id, CancellationToken cancellationToken);

        Task<IResult<Article>> Update(Article article, CancellationToken cancellationToken);

        Task<IResult<IEnumerable<Article>>> GetAll(CancellationToken cancellationToken);

        Task<IResult<Article>> GetById(Guid id, CancellationToken cancellationToken);
        Task<IResult<Article>> GetBySeoUrl(string seoUrl, CancellationToken cancellationToken);

        Task<IResult<IEnumerable<Article>>> GetByCategoryId(Guid categoryId, CancellationToken cancellationToken);
    }
}
