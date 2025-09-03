using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using OvgBlog.Business.Abstract;
using OvgBlog.Business.Constants;
using OvgBlog.Business.Dto;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Extensions;

namespace OvgBlog.Business.Services;

public class HomeService(IArticleRepository articleRepository, ICategoryRepository categoryRepository)
    : IHomeService
{
    public async Task<HomeDto> GetHomeAsync(CancellationToken cancellationToken)
    {
        var articlesEntity = await articleRepository.GetFilteredArticlesAsync(HomeConstants.ArticleLimit, cancellationToken);
        articlesEntity.ForEach(item => item.Body = item.Body?.Length > HomeConstants.ArticleBodyLimit
            ? (item.Body?[..HomeConstants.ArticleBodyRetrieveLength]?.ToString() ?? string.Empty) + CoreConstants.Ellipsis
            : item.Body);
        
        var categoriesEntity = await categoryRepository.GetAllAsync(cancellationToken);
        foreach (var category in categoriesEntity.IfNullOrEmpty())
        {
            if(category.ImageUrl == null) continue;

            category.ImageUrl = ImageUrlConstants.CategoryImageUrl;
        }
        
        var articles = articlesEntity.Adapt<List<ArticleDto>>();
        var categories = categoriesEntity.Adapt<List<CategoryDto>>();
            
        var response = new HomeDto(articles, categories);
        return response;
    }
}