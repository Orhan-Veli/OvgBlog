using System;
using OvgBlog.Business.Dto.Base;

namespace OvgBlog.Business.Dto;

public class ArticleCategoryRelationDto : BaseResponseDto
{
    public Guid CategoryId { get; set; }
    public Guid ArticleId { get; set; }

    public ArticleDto Article { get; set; }
    public CategoryDto Category { get; set; }
}