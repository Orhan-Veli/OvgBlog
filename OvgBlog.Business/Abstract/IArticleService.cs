using OvgBlog.Business.Constants;
using OvgBlog.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OvgBlog.Business.Abstract
{
    public interface IArticleService
    {
        Task<IResult<Article>> Create(Article article);

        Task<IResult<object>> Delete(Guid id);

        Task<IResult<Article>> Update(Article article);

        Task<IResult<IEnumerable<Article>>> GetAll();

        Task<IResult<Article>> GetById(Guid id);
    }
}
