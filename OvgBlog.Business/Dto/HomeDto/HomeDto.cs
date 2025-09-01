using System.Collections.Generic;

namespace OvgBlog.Business.Dto;

public class HomeDto
{
    public HomeDto(List<ArticleDto> articles, List<CategoryDto> categories)
    {
        Articles = articles;
        Categories = categories;
    }

    public HomeDto()
    {
        Articles = [];
        Categories = [];
    }
    
    public List<ArticleDto> Articles { get; set; }
    public List<CategoryDto> Categories { get; set; }
}